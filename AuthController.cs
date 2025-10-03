using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using APPR6312PART2.Models;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312PART2.Controllers
{
    public class AuthController : Controller
    {
        // Static list to store users (replace with database in real application)
        private static List<User> _users = new List<User>();
        private static int _nextUserId = 1;

        // Constructor to create default admin user
        public AuthController()
        {
            CreateDefaultAdmin();
        }

        // Registration Page
        public IActionResult Register()
        {
            return View();
        }

        // Handle Registration Form Submission
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (_users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already registered");
                    return View(user);
                }

                // Assign user ID and add to list
                user.UserId = _nextUserId++;
                _users.Add(user);

                // Also add to HomeController's user list for admin dashboard access
                var homeUsers = HomeController.GetUsers();
                homeUsers.Add(user);

                // Store user in session
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");
                HttpContext.Session.SetString("UserType", user.UserType);

                TempData["SuccessMessage"] = "Registration successful! Welcome to Disaster Alleviation Foundation.";
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        // Login Page
        public IActionResult Login()
        {
            return View();
        }

        // Handle Login Form Submission
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password && u.IsActive);

                if (user != null)
                {
                    // Store user in session
                    HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");
                    HttpContext.Session.SetString("UserType", user.UserType);

                    TempData["SuccessMessage"] = $"Welcome back, {user.FirstName}!";
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid email or password");
            }

            return View(loginModel);
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Index", "Home");
        }

        // User Profile
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Please log in to view your profile.";
                return RedirectToAction("Login");
            }

            var user = _users.FirstOrDefault(u => u.UserId.ToString() == userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // Update Profile
        [HttpPost]
        public IActionResult Profile(User updatedUser)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString("UserId");
                var existingUser = _users.FirstOrDefault(u => u.UserId.ToString() == userId);

                if (existingUser != null)
                {
                    // Update user details
                    existingUser.FirstName = updatedUser.FirstName;
                    existingUser.LastName = updatedUser.LastName;
                    existingUser.PhoneNumber = updatedUser.PhoneNumber;
                    existingUser.UserType = updatedUser.UserType;

                    // Update session
                    HttpContext.Session.SetString("UserName", $"{updatedUser.FirstName} {updatedUser.LastName}");
                    HttpContext.Session.SetString("UserType", updatedUser.UserType);

                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToAction("Profile");
                }
            }

            return View(updatedUser);
        }

        // Forgot Password Page
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // Handle Forgot Password Form Submission
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Please enter your email address");
                return View();
            }

            // In a real application, you would:
            // 1. Check if email exists in the database
            // 2. Generate a password reset token
            // 3. Send an email with reset link
            // 4. Store the token in the database with expiration

            // For demo purposes, we'll just show a success message
            TempData["SuccessMessage"] = "Password reset link has been sent to your email. Please check your inbox (and spam folder).";
            return RedirectToAction("Login");
        }

        // Reset Password Page
        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Invalid reset token.";
                return RedirectToAction("ForgotPassword");
            }

            // In real application, verify token validity
            ViewBag.Token = token;
            return View();
        }

        // Handle Reset Password Form Submission
        [HttpPost]
        public IActionResult ResetPassword(string token, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View();
            }

            if (newPassword.Length < 6)
            {
                ModelState.AddModelError("", "Password must be at least 6 characters long.");
                return View();
            }

            // In a real application, you would:
            // 1. Verify the token is valid and not expired
            // 2. Update the user's password in the database
            // 3. Invalidate the used token

            TempData["SuccessMessage"] = "Your password has been reset successfully. You can now login with your new password.";
            return RedirectToAction("Login");
        }

        // Change Password Page (for logged-in users)
        public IActionResult ChangePassword()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Please log in to change your password.";
                return RedirectToAction("Login");
            }

            return View();
        }

        // Handle Change Password Form Submission
        [HttpPost]
        public IActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Please log in to change your password.";
                return RedirectToAction("Login");
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "New passwords do not match.");
                return View();
            }

            if (newPassword.Length < 6)
            {
                ModelState.AddModelError("", "New password must be at least 6 characters long.");
                return View();
            }

            var user = _users.FirstOrDefault(u => u.UserId.ToString() == userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Login");
            }

            // Verify current password
            if (user.Password != currentPassword)
            {
                ModelState.AddModelError("", "Current password is incorrect.");
                return View();
            }

            // Update password
            user.Password = newPassword;

            TempData["SuccessMessage"] = "Your password has been changed successfully.";
            return RedirectToAction("Profile");
        }

        // Method to create default admin user
        private void CreateDefaultAdmin()
        {
            if (!_users.Any(u => u.Email == "admin@disasteralleviation.org"))
            {
                var adminUser = new User
                {
                    UserId = _nextUserId++,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@disasteralleviation.org",
                    Password = "admin123", // In real app, this should be hashed
                    PhoneNumber = "012-345-6789",
                    UserType = "Admin",
                    CreatedAt = System.DateTime.Now,
                    IsActive = true
                };
                _users.Add(adminUser);

                // Also add to HomeController's user list
                var homeUsers = HomeController.GetUsers();
                homeUsers.Add(adminUser);
            }
        }

        // Static method to access users from other controllers
        public static List<User> GetUsers()
        {
            return _users;
        }
    }
}