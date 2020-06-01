namespace SesNotifications.App.Models
{
    public class SesSendEventModel : Ses
    {
        public virtual SesMail Mail { get; set; }
        public virtual SesSendEvent Send { get; set; }
    }
}