using Microsoft.AspNetCore.Mvc;
using APPR6312PART2.Models;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312PART2.Controllers
{
    public class VolunteerController : Controller
    {
        // Static list to store volunteers (replace with database in real application)
        private static List<Volunteer> _volunteers = new List<Volunteer>();
        private static int _nextVolunteerId = 1;

        // Volunteer Registration Page
        public IActionResult Register()
        {
            return View();
        }

        // Handle Volunteer Registration Submission
        [HttpPost]
        public IActionResult Register(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                // Assign ID and add to list
                volunteer.VolunteerId = _nextVolunteerId++;
                _volunteers.Add(volunteer);

                TempData["SuccessMessage"] = "Thank you for registering as a volunteer! We will contact you shortly with available opportunities.";
                return RedirectToAction("Index", "Home");
            }

            return View(volunteer);
        }

        // View All Volunteers (Admin view)
        public IActionResult ViewVolunteers()
        {
            return View(_volunteers.OrderByDescending(v => v.RegistrationDate).ToList());
        }

        // Volunteer Details
        public IActionResult Details(int id)
        {
            var volunteer = _volunteers.FirstOrDefault(v => v.VolunteerId == id);
            if (volunteer == null)
            {
                return NotFound();
            }
            return View(volunteer);
        }

        // Available Tasks for Volunteers
        public IActionResult AvailableTasks()
        {
            var tasks = new List<object>
            {
                new {
                    Id = 1,
                    Title = "Disaster Relief Distribution",
                    Description = "Help distribute food, water, and supplies to affected communities",
                    Location = "Various disaster sites",
                    Duration = "4-8 hours",
                    SkillsRequired = "Physical fitness, compassion",
                    Urgency = "High"
                },
                new {
                    Id = 2,
                    Title = "Shelter Setup and Management",
                    Description = "Assist in setting up and managing temporary shelters for displaced families",
                    Location = "Designated shelter locations",
                    Duration = "6-12 hours",
                    SkillsRequired = "Organization, teamwork",
                    Urgency = "High"
                },
                new {
                    Id = 3,
                    Title = "Medical Support Assistant",
                    Description = "Support medical teams with non-medical tasks and patient care",
                    Location = "Field hospitals and clinics",
                    Duration = "8 hours",
                    SkillsRequired = "First aid training preferred",
                    Urgency = "Medium"
                },
                new {
                    Id = 4,
                    Title = "Communication and Coordination",
                    Description = "Help with communication between teams and coordinate relief efforts",
                    Location = "Command center",
                    Duration = "4-6 hours",
                    SkillsRequired = "Communication skills, calm under pressure",
                    Urgency = "Medium"
                },
                new {
                    Id = 5,
                    Title = "Donation Sorting and Management",
                    Description = "Sort and organize donated items for efficient distribution",
                    Location = "Warehouse facilities",
                    Duration = "4 hours",
                    SkillsRequired = "Organization, attention to detail",
                    Urgency = "Low"
                }
            };

            return View(tasks);
        }

        // Volunteer Impact Statistics
        public IActionResult VolunteerImpact()
        {
            var impactStats = new
            {
                TotalVolunteers = _volunteers.Count,
                ActiveVolunteers = _volunteers.Count(v => v.Status == "Active"),
                NewVolunteers = _volunteers.Count(v => v.RegistrationDate >= DateTime.Now.AddDays(-30)),
                SkillsBreakdown = _volunteers.GroupBy(v => v.Skills)
                                            .Select(g => new { Skill = g.Key, Count = g.Count() })
                                            .ToList()
            };

            return View(impactStats);
        }
    }
}