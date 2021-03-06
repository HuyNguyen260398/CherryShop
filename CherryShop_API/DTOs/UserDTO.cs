using System.ComponentModel.DataAnnotations;

namespace CherryShop_API.DTOs
{
    public class UserDTO
    {
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Your password is limited to {2} to {1} characters", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
