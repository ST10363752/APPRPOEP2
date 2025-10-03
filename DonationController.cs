using Microsoft.AspNetCore.Mvc;
using APPR6312PART2.Models;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312PART2.Controllers
{
    public class DonationController : Controller
    {
        // Static list to store donations (replace with database in real application)
        private static List<Donation> _donations = new List<Donation>();
        private static int _nextDonationId = 1;

        // Donate Page
        public IActionResult Donate()
        {
            return View();
        }

        // Handle Donation Submission
        [HttpPost]
        public IActionResult Donate(Donation donation)
        {
            if (ModelState.IsValid)
            {
                // Assign ID and add to list
                donation.DonationId = _nextDonationId++;
                _donations.Add(donation);

                TempData["SuccessMessage"] = "Thank you for your generous donation! We will contact you shortly to coordinate delivery.";
                return RedirectToAction("Index", "Home");
            }

            return View(donation);
        }

        // View All Donations
        public IActionResult ViewDonations()
        {
            return View(_donations.OrderByDescending(d => d.DonationDate).ToList());
        }

        // Donation Details
        public IActionResult Details(int id)
        {
            var donation = _donations.FirstOrDefault(d => d.DonationId == id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // Track Donation Impact
        public IActionResult Impact()
        {
            // Create a simple view model to avoid null reference issues
            var impactData = new
            {
                TotalDonations = _donations.Count,
                TotalMonetary = _donations.Where(d => d.MonetaryAmount.HasValue).Sum(d => d.MonetaryAmount ?? 0),
                TotalItems = _donations.Where(d => d.Quantity.HasValue).Sum(d => d.Quantity ?? 0),
                RecentDonations = _donations.OrderByDescending(d => d.DonationDate).Take(5).ToList()
            };

            return View(impactData);
        }
    }
}