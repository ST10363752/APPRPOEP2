using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using APPR6312PART2.Models;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312PART2.Controllers
{
    public class DashboardController : Controller
    {
        // Reference to our static data from other controllers
        private static List<User> _users = new List<User>();
        private static List<Disaster> _disasters = new List<Disaster>();
        private static List<Donation> _donations = new List<Donation>();
        private static List<Volunteer> _volunteers = new List<Volunteer>();

        // Main Dashboard
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Please log in to access the dashboard.";
                return RedirectToAction("Login", "Auth");
            }

            // Get user data from AuthController (in real app, this would be from a service)
            var userData = GetUserData();
            var disasterData = GetDisasterData();
            var donationData = GetDonationData();
            var volunteerData = GetVolunteerData();

            var dashboardData = new
            {
                UserStats = new
                {
                    TotalUsers = userData.Count,
                    NewUsers = userData.Count(u => u.CreatedAt >= System.DateTime.Now.AddDays(-30)),
                    ActiveUsers = userData.Count(u => u.IsActive)
                },
                DisasterStats = new
                {
                    TotalDisasters = disasterData.Count,
                    ActiveDisasters = disasterData.Count(d => d.EndDate == null || d.EndDate > System.DateTime.Now),
                    CriticalDisasters = disasterData.Count(d => d.Severity == "Critical")
                },
                DonationStats = new
                {
                    TotalDonations = donationData.Count,
                    TotalMonetary = donationData.Where(d => d.MonetaryAmount.HasValue).Sum(d => d.MonetaryAmount.Value),
                    UrgentDonations = donationData.Count(d => d.IsUrgent)
                },
                VolunteerStats = new
                {
                    TotalVolunteers = volunteerData.Count,
                    ActiveVolunteers = volunteerData.Count(v => v.Status == "Active"),
                    AvailableTasks = 5 // From VolunteerController
                },
                RecentActivity = GetRecentActivity()
            };

            return View(dashboardData);
        }

        // Admin User Management
        public IActionResult Users()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Please log in to access user management.";
                return RedirectToAction("Login", "Auth");
            }

            var users = GetUserData();
            return View(users);
        }

        // System Analytics
        public IActionResult Analytics()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Please log in to access analytics.";
                return RedirectToAction("Login", "Auth");
            }

            var analyticsData = new
            {
                MonthlyDonations = new[] { 12000, 19000, 15000, 25000, 22000, 30000, 28000, 26000, 31000, 29000, 33000, 36000 },
                DisasterTrends = new[] { 5, 8, 6, 12, 15, 10, 8, 14, 11, 9, 13, 16 },
                VolunteerGrowth = new[] { 50, 75, 100, 125, 150, 180, 210, 240, 270, 300, 330, 360 },
                TopDonors = GetTopDonors(),
                DisasterTypes = GetDisasterTypeDistribution()
            };

            return View(analyticsData);
        }

        // Settings Page
        public IActionResult Settings()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Please log in to access settings.";
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        // Helper methods to get data (in real app, these would be from database)
        private List<User> GetUserData()
        {
            // This would normally come from a database
            // For now, return empty list - data will be populated when users register
            return _users;
        }

        private List<Disaster> GetDisasterData()
        {
            // This would normally come from a database
            return _disasters;
        }

        private List<Donation> GetDonationData()
        {
            // This would normally come from a database
            return _donations;
        }

        private List<Volunteer> GetVolunteerData()
        {
            // This would normally come from a database
            return _volunteers;
        }

        private List<object> GetRecentActivity()
        {
            var activities = new List<object>
            {
                new { Type = "Donation", Description = "New donation received", Time = "2 hours ago", Icon = "❤️" },
                new { Type = "Disaster", Description = "Flood reported in Coastal Area", Time = "5 hours ago", Icon = "🌋" },
                new { Type = "Volunteer", Description = "New volunteer registered", Time = "1 day ago", Icon = "🤝" },
                new { Type = "User", Description = "New user account created", Time = "1 day ago", Icon = "👤" },
                new { Type = "Donation", Description = "Medical supplies donation", Time = "2 days ago", Icon = "❤️" }
            };

            return activities;
        }

        private List<object> GetTopDonors()
        {
            return new List<object>
            {
                new { Name = "Wandile Phakathi", Amount = 5000, Donations = 12 },
                new { Name = "Siyanqoba Zuma", Amount = 3500, Donations = 8 },
                new { Name = "Landile Fakazi", Amount = 2800, Donations = 15 },
                new { Name = "Gcina Hlongwa", Amount = 2200, Donations = 6 },
                new { Name = "Amahle Mhlophe", Amount = 1800, Donations = 10 }
            };
        }

        private List<object> GetDisasterTypeDistribution()
        {
            return new List<object>
            {
                new { Type = "Flood", Count = 20, Percentage = 28 },
                new { Type = "Earthquake", Count = 18, Percentage = 25 },
                new { Type = "Wildfire", Count = 12, Percentage = 17 },
                new { Type = "Hurricane", Count = 8, Percentage = 11 },
                new { Type = "War", Count = 7, Percentage = 10 },
                new { Type = "Other", Count = 6, Percentage = 9 }
            };
        }
    }
}