using CorePush.Google;
using TargetChatServer11.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TargetChatServer11.Interfaces;
using TargetChatServer11.Utils;
using FirebaseAdmin.Messaging;
using TargetChatServer11.Data;
using Microsoft.EntityFrameworkCore;
using Message = TargetChatServer11.Models.Message;

namespace TargetChatServer11.Service
{
    public class NotificationRepositroy : INotificationRepository
    {
        private readonly TargetChatServer11Context _context;
        private readonly IUserRepository _user;
        public NotificationRepositroy(TargetChatServer11Context context, IUserRepository user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Message?> SendNotification(Message message, string username)
        {
            var sent = "false";
            var user = await _user.GetUserByUsername(username);
            var checkIfExist = await _context.AndroidDeviceIDModel.Where(item => item.User.Username.Equals(username)).Select(item => item.DeviceId).ToListAsync();
            if (!checkIfExist.Any())
            {
                return null;
            }

            if (message.Sent)
            {
                sent = "true";
            }

            ResponseModel response = new ResponseModel();
            try
            {
                var message_tosend = new MulticastMessage()
                {
                    Data = new Dictionary<string, string>()
                    {
                        { "content", message.Content },
                        { "created", message.Date },
                        { "sent", sent }
                    },

                    Tokens = checkIfExist
                };

                var messaging = FirebaseMessaging.DefaultInstance;
                var fcmSendResponse = await messaging.SendMulticastAsync(message_tosend);
                return message;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AndroidDeviceIDModel?> CreateAndoridDeviceOfUser(AndroidDeviceIDModel androidDeviceIDModel, string username)
        {
            var user = await _user.GetUserByUsername(username);
            androidDeviceIDModel.User = user;
            var checkIfExist = await _context.AndroidDeviceIDModel.FirstOrDefaultAsync(item => item.Id == androidDeviceIDModel.Id);
            if (checkIfExist != null)
            {
                _context.AndroidDeviceIDModel.Remove(checkIfExist);
            }

            _context.AndroidDeviceIDModel.Add(androidDeviceIDModel);
            await _context.SaveChangesAsync();
            return androidDeviceIDModel;
        }

        public Task<ResponseModel> SendNotification(NotificationModel notificationModel)
        {
            throw new NotImplementedException();
        }
    }
}