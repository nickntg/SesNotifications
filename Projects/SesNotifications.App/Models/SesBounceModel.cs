namespace SesNotifications.App.Models
{
    public class SesBounceModel : Ses
    {
        public virtual SesMail Mail { get; set; }
        public virtual SesBounce Bounce { get; set; }
    }
}