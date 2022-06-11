using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TargetChatServer11.Models
{
    public class Contact
    {
        [Key]
        [IgnoreDataMember]
        public int Identifier { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        public string displayName { get; set; }
        [Required]
        public string server { get; set; }
        public string? last { get; set; }
        [DataType(DataType.Date)]
        public DateTime? lastdate { get; set; }
        [Required]
        [IgnoreDataMember]
        public UserModel User { get; set; }
        [IgnoreDataMember]
        public List<Message>? Messages { get; set; }
    }
}
