using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CRUDAPI.Models
{
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [StringLength(11)]
        public string number { get; set; }

    }
}
