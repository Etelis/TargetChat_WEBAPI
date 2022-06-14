using TargetChatServer11.Models;
using TargetChatServer11.Utils;

namespace TargetChatServer11.Interfaces
{
    public interface INotificationRepository
    {
        Task<Message?> SendNotification(Message message, string username);
        Task<AndroidDeviceIDModel?> CreateAndoridDeviceOfUser(AndroidDeviceIDModel androidDeviceIDModel, string username);
    }
}
