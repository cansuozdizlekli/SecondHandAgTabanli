using System;
using System.ComponentModel.DataAnnotations;

namespace SecondHandMVC.Models
{
    //[Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Email field is required")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name field is required")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Password { get; set; }

        public ICollection<Advertisement> MyAdvertisements { get; set; }
        public ICollection<Advertisement> MyPurchases { get; set; }

    }

}

