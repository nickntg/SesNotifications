namespace SesNotifications.DataAccess.Entities
{
    public class MonitorRule
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string SesMessage { get; set; }
        public virtual string JsonMatcher { get; set; }
        public virtual string Regex { get; set; }
    }
}