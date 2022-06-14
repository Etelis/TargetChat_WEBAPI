namespace TargetChatServer11.Utils
{
    public class ContactUpdate
    {
        public string name { get; set; }
        public string server { get; set; }
    }

    public class ContactToPost
    {
        public string id { get; set; }
        public string name { get; set; }
        public string server { get; set; }
        public string? last { get; set; }
        public DateTime? lastdate { get; set; }
    }

        public class ContentToPost
        {
            public string content { get; set; }
        }

}
