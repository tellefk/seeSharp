using System.ComponentModel.DataAnnotations;

namespace DotnetAPI.Models
{
    public class User
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name is too long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name is too long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("^(male|female)$", ErrorMessage = "Invalid gender")]
        public string Gender { get; set; }

        public bool Active { get; set; }
    }


    public class UserDTO
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }

        public bool Active { get; set; }


        public UserDTO()
        {

            if (FirstName == null)
            {
                FirstName = "";
            }

            if (LastName == null)
            {
                LastName = "";
            }

            if (Email != null && !Email.Contains("@"))
            {
                throw new ArgumentException("Invalid email format");
            }

            if (Gender != null && Gender != "female" && Gender != "male")
            {
                throw new ArgumentException("Invalid gender");
            }

        }

    }
}