using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TargetChatServer11.Models
{
    public class UserModel
    {
        [IgnoreDataMember]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [IgnoreDataMember]
        public string Password { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public string? Photo { get; set; }
        [IgnoreDataMember]
        public List<Contact>? Contacts { get; set; }
        [IgnoreDataMember]
        public List<AndroidDeviceIDModel>? DeviceIds { get; set; }
    }
}
