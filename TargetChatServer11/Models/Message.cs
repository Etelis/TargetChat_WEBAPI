using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TargetChatServer11.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        [IgnoreDataMember]
        public Contact Contact { get; set; }
        [Required]
        public bool Sent { get; set; }
    }
}
