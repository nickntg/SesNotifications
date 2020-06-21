namespace SesNotifications.App.Models
{
    public class SqsConfiguration
    {
        public virtual string QueueUrl { get; set; }
        public virtual string Region { get; set; }
        public virtual string AccessKey { get; set; }
        public virtual string SecretKey { get; set; }
    }
}