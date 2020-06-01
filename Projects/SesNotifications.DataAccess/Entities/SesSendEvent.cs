namespace SesNotifications.DataAccess.Entities
{
    public class SesSendEvent : SesCommon
    {
        public virtual string Recipients { get; set; }
    }
}