using System.ComponentModel.DataAnnotations;

namespace Server_Application.BusinessEntities.ViewModels
{
    public class LoginVM
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}