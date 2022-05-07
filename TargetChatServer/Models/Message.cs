using System.ComponentModel.DataAnnotations;

namespace targetchatserver.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Contact Contact { get; set; }
        [Required]
        public bool Sent { get; set; }
    }
}
