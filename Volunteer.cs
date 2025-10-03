using System.ComponentModel.DataAnnotations;

namespace APPR6312PART2.Models
{
    public class Volunteer
    {
        [Key]
        public int VolunteerId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State/Province is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please select your availability")]
        [Display(Name = "Availability")]
        public string Availability { get; set; }

        [Required(ErrorMessage = "Please select your skills")]
        [Display(Name = "Skills")]
        public string Skills { get; set; }

        [Display(Name = "Previous Experience")]
        [StringLength(500, ErrorMessage = "Experience description cannot exceed 500 characters")]
        public string PreviousExperience { get; set; }

        [Required(ErrorMessage = "Emergency contact name is required")]
        [Display(Name = "Emergency Contact Name")]
        public string EmergencyContactName { get; set; }

        [Required(ErrorMessage = "Emergency contact phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Emergency Contact Phone")]
        public string EmergencyContactPhone { get; set; }

        [Display(Name = "Medical Conditions")]
        [StringLength(200, ErrorMessage = "Medical conditions cannot exceed 200 characters")]
        public string MedicalConditions { get; set; }

        [Display(Name = "Special Requirements")]
        [StringLength(200, ErrorMessage = "Special requirements cannot exceed 200 characters")]
        public string SpecialRequirements { get; set; }

        [Required(ErrorMessage = "You must agree to the terms and conditions")]
        [Display(Name = "I agree to the terms and conditions")]
        public bool AgreedToTerms { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending";

        [Display(Name = "Assigned Tasks")]
        public string AssignedTasks { get; set; } = "Not assigned yet";
    }
}