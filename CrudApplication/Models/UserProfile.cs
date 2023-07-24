using System.ComponentModel.DataAnnotations;

namespace CRUDAPI.Models
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string number { get; set; }
    }
}
