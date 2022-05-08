using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace targetchatserver.Models
{
    public class Contact
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string Server { get; set; }
        [Required]
        public string LastMessage { get; set; }
        [Required]
        public DateTime LastDate { get; set; }
        [Required]
        [IgnoreDataMember]
        [Key]
        public UserModel User { get; set; }
        [IgnoreDataMember]
        public List<Message>? Messages { get; set; }

    }
}
