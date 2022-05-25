namespace TargetChatServer11.Utils
{
    public class TransferContact
    {
        public string name { get; set; }
        public string id { get; set; }
        public string server { get; set; }
    }

    public class TransferMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string content { get; set; }
    }
}
