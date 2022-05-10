namespace targetchatserver.Models
{
    public class MessageToPost
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool Sent { get; set; }
    }
}
