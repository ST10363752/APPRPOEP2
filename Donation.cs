using System.ComponentModel.DataAnnotations;

namespace APPR6312PART2.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [Required(ErrorMessage = "Donor name is required")]
        [Display(Name = "Donor Name")]
        public string DonorName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Donation type is required")]
        [Display(Name = "Donation Type")]
        public string DonationType { get; set; }

        [Display(Name = "Item Description")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string ItemDescription { get; set; }

        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int? Quantity { get; set; }

        [Display(Name = "Monetary Amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal? MonetaryAmount { get; set; }

        [Required(ErrorMessage = "Please specify where to allocate this donation")]
        [Display(Name = "Allocation Preference")]
        public string AllocationPreference { get; set; }

        [Display(Name = "Urgent Delivery Required")]
        public bool IsUrgent { get; set; }

        [Display(Name = "Delivery/Pickup Date")]
        [DataType(DataType.Date)]
        public DateTime? PreferredDate { get; set; }

        [Display(Name = "Additional Notes")]
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string AdditionalNotes { get; set; }

        public DateTime DonationDate { get; set; } = DateTime.Now;

        [Display(Name = "Donation Status")]
        public string Status { get; set; } = "Pending";
    }
}