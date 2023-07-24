using System.ComponentModel.DataAnnotations;

namespace CRUDAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = ("username is required"))]
        public string Username { get; set; }
        [Required(ErrorMessage = ("password is required"))]
        public string Password { get; set; }
        [Required(ErrorMessage = ("email is required"))]
        public string Email { get; set; }
        public bool isActive { get; set; }


    }
}
