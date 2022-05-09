using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace targetchatserver.Models
{
    public class Contact
    {
        [Key]
        public string id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string server { get; set; }
        public string? last { get; set; }
        public DateTime? lastdate { get; set; }
        [Required]
        [IgnoreDataMember]
        [Key]
        public UserModel User { get; set; }
        [IgnoreDataMember]
        public List<Message>? Messages { get; set; }
    }
}
