using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TargetChatServer11.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public string? Photo { get; set; }
        public List<Contact>? Contacts { get; set; }
        [IgnoreDataMember]
        public List<AndroidDeviceIDModel>? DeviceIds { get; set; }
    }
}
