using TargetChatServer11.Models;

namespace TargetChatServer11.Utils
{
    public class Session
    {
        public UserModel user { get; set; }
        public string token { get; set; }
    }

    public class UserConnection
    {
        public string username { get; set; }
        public string contactID { get; set; }
    }

    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
