using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TargetChatServer11.Models
{
    public class Message
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public string created { get; set; }
        [Required]
        [IgnoreDataMember]
        public Contact contact { get; set; }
        [Required]
        public bool sent { get; set; }
    }
}
