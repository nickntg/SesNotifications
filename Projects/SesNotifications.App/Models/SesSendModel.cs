namespace SesNotifications.App.Models
{
    public class SesSendModel : Ses
    {
        public virtual SesMail Mail { get; set; }
        public virtual SesSend Send { get; set; }
    }
}