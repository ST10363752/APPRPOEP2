using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using APPR6312PART2.Models;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312PART2.Controllers
{
    public class HomeController : Controller
    {
        // Static lists to store data (replace with database in real application)
        private static List<User> _users = new List<User>();
        private static List<Disaster> _disasters = new List<Disaster>();
        private static List<Donation> _donations = new List<Donation>();
        private static List<Volunteer> _volunteers = new List<Volunteer>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            var model = new AboutViewModel
            {
                Mission = "To provide immediate relief and long-term support to communities affected by natural disasters and emergencies through coordinated efforts, resource distribution, and volunteer mobilization.",
                Vision = "A world where no community suffers unnecessarily from natural disasters, and where help arrives swiftly and effectively when disaster strikes.",
                History = "Founded in 2010, Disaster Alleviation Foundation has grown from a small local initiative to an international organization responding to disasters across 50+ countries. Our journey began with a single flood response team and has expanded to include comprehensive disaster management services.",
                TeamMembers = new List<TeamMember>
                {
                    new TeamMember
                    {
                        Name = "Dr. Sarah Johnson",
                        Position = "Executive Director",
                        Description = "20+ years in disaster management and humanitarian aid",
                        Email = "sarah.johnson@disasteralleviation.org"
                    },
                    new TeamMember
                    {
                        Name = "Michael Chen",
                        Position = "Operations Director",
                        Description = "Specializes in logistics and emergency response coordination",
                        Email = "michael.chen@disasteralleviation.org"
                    },
                    new TeamMember
                    {
                        Name = "Maria Rodriguez",
                        Position = "Volunteer Coordinator",
                        Description = "Manages our global network of 5,000+ volunteers",
                        Email = "maria.rodriguez@disasteralleviation.org"
                    },
                    new TeamMember
                    {
                        Name = "David Thompson",
                        Position = "Medical Director",
                        Description = "Leads our medical response teams and health initiatives",
                        Email = "david.thompson@disasteralleviation.org"
                    }
                },
                Achievements = new List<Achievement>
                {
                    new Achievement
                    {
                        Year = "2023",
                        Title = "Global Response Excellence Award",
                        Description = "Recognized for outstanding disaster response across 15 countries"
                    },
                    new Achievement
                    {
                        Year = "2022",
                        Title = "UN Partnership Certification",
                        Description = "Certified as a key partner in United Nations disaster response"
                    },
                    new Achievement
                    {
                        Year = "2021",
                        Title = "Community Impact Award",
                        Description = "Awarded for significant positive impact on 500,000+ lives"
                    },
                    new Achievement
                    {
                        Year = "2020",
                        Title = "Innovation in Disaster Tech",
                        Description = "Recognized for developing innovative disaster management technology"
                    }
                }
            };

            return View(model);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }

        // Admin Dashboard
        public IActionResult AdminDashboard()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Please log in to access the admin dashboard.";
                return RedirectToAction("Login", "Auth");
            }

            // Get user data directly from our static list
            var user = _users.FirstOrDefault(u => u.UserId.ToString() == userId);
            if (user == null || user.UserType != "Admin")
            {
                TempData["ErrorMessage"] = "Admin privileges required to access this page.";
                return RedirectToAction("Index", "Home");
            }

            var adminData = new
            {
                SystemStats = new
                {
                    TotalUsers = _users.Count,
                    ActiveDisasters = _disasters.Count(d => d.EndDate == null || d.EndDate > System.DateTime.Now),
                    PendingDonations = _donations.Count(d => d.Status == "Pending"),
                    ActiveVolunteers = _volunteers.Count(v => v.Status == "Active"),
                    SystemUptime = "99.9%",
                    StorageUsed = "2.3GB/10GB"
                },
                RecentActivities = new[]
                {
                    new { Action = "User Registration", User = "john@email.com", Time = "2 minutes ago", Status = "Completed" },
                    new { Action = "Disaster Report", User = "sarah@email.com", Time = "5 minutes ago", Status = "Pending" },
                    new { Action = "Donation Processed", User = "mike@email.com", Time = "10 minutes ago", Status = "Completed" },
                    new { Action = "System Backup", User = "System", Time = "15 minutes ago", Status = "Success" }
                },
                SystemHealth = new
                {
                    CpuUsage = 45,
                    MemoryUsage = 62,
                    DiskUsage = 78,
                    DatabaseStatus = "Optimal"
                }
            };

            return View(adminData);
        }

        // Static methods to access data from other controllers
        public static List<User> GetUsers()
        {
            return _users;
        }

        public static List<Disaster> GetDisasters()
        {
            return _disasters;
        }

        public static List<Donation> GetDonations()
        {
            return _donations;
        }

        public static List<Volunteer> GetVolunteers()
        {
            return _volunteers;
        }

        // Helper methods to add data from other controllers
        public static void AddDisaster(Disaster disaster)
        {
            _disasters.Add(disaster);
        }

        public static void AddDonation(Donation donation)
        {
            _donations.Add(donation);
        }

        public static void AddVolunteer(Volunteer volunteer)
        {
            _volunteers.Add(volunteer);
        }
    }
}