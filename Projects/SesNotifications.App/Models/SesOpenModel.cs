namespace SesNotifications.App.Models
{
    public class SesOpenModel : Ses
    {
        public virtual SesMail Mail { get; set; }
        public virtual SesOpen Open { get; set; }
    }
}