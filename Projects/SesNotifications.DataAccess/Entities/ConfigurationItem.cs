namespace SesNotifications.DataAccess.Entities
{
    public class ConfigurationItem
    {
        public virtual int Id { get; set; }
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
    }
}