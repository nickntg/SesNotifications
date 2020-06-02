namespace SesNotifications.App.Models
{
    public class SesBounceEventModel : Ses
    {
        public virtual SesMail Mail { get; set; }
        public virtual SesBounceEvent Bounce { get; set; }
    }
}