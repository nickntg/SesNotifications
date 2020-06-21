namespace SesNotifications.App.Models
{
    public class MonitorRule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SesMessage { get; set; }
        public string JsonMatcher { get; set; }
        public string Regex { get; set; }
    }
}