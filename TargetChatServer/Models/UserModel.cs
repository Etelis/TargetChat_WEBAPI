using System.ComponentModel.DataAnnotations;

namespace targetchatserver.Models
{
    public class UserModel
    {
        [Key]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Photo { get; set; }
        public List<Contact>? Contacts { get; set; }

    }
}
