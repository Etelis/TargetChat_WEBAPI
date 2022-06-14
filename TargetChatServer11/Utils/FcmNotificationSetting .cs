using Newtonsoft.Json;

namespace TargetChatServer11.Utils
{
    public class FcmNotificationSetting
    {
        public string SenderId { get; set; }
        public string ServerKey { get; set; }
    }
    public class ResponseModel
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class NotificationModel
    {
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }

}
