namespace SesNotifications.App.Models
{
    public class SesOpenEventModel : Ses
    {
        public virtual SesMail Mail { get; set; }
        public virtual SesOpenEvent Open { get; set; }
    }
}