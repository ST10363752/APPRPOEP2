using Microsoft.AspNetCore.Mvc;
using APPR6312PART2.Models;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312PART2.Controllers
{
    public class DisasterController : Controller
    {
        // Static list to store disasters (replace with database in real application)
        private static List<Disaster> _disasters = new List<Disaster>();
        private static int _nextDisasterId = 1;

        // Report Disaster Page
        public IActionResult Report()
        {
            return View();
        }

        // Handle Disaster Report Submission
        [HttpPost]
        public IActionResult Report(Disaster disaster)
        {
            if (ModelState.IsValid)
            {
                // Assign ID and add to list
                disaster.DisasterId = _nextDisasterId++;
                _disasters.Add(disaster);

                TempData["SuccessMessage"] = "Disaster incident reported successfully! Our team will review it immediately.";
                return RedirectToAction("Index", "Home");
            }

            return View(disaster);
        }

        // View All Disasters
        public IActionResult ViewDisasters()
        {
            return View(_disasters.OrderByDescending(d => d.ReportedAt).ToList());
        }

        // Disaster Details
        public IActionResult Details(int id)
        {
            var disaster = _disasters.FirstOrDefault(d => d.DisasterId == id);
            if (disaster == null)
            {
                return NotFound();
            }
            return View(disaster);
        }
    }
}