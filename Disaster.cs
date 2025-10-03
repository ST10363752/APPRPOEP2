using System.ComponentModel.DataAnnotations;

namespace APPR6312PART2.Models
{
    public class Disaster
    {
        [Key]
        public int DisasterId { get; set; }

        [Required(ErrorMessage = "Disaster type is required")]
        [Display(Name = "Disaster Type")]
        public string DisasterType { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Severity level is required")]
        public string Severity { get; set; }

        [Display(Name = "Required Aid Type")]
        public string RequiredAidType { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Contact Phone")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string ContactPhone { get; set; }

        public DateTime ReportedAt { get; set; } = DateTime.Now;

        [Display(Name = "Number of People Affected")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int AffectedPeople { get; set; } = 1;
    }
}