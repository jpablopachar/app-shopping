namespace email.SendGrid.Models
{
    public class SendGridData
    {
        public string? SendGridApiKey { get; set; }
        public string? EmailTo { get; set; }
        public string? NameTo { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}