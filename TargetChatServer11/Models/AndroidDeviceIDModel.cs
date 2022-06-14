using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TargetChatServer11.Models
{
    public class AndroidDeviceIDModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [JsonProperty("deviceId")]
        public string? DeviceId { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public UserModel? User { get; set; }
    }
}
