namespace targetchatserver.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Server { get; set; }
        public string LastMessage { get; set; }
        public DateOnly LastDate { get; set; }

    }
}
