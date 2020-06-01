namespace SesNotifications.App.Models
{
    public class SesDeliveryEventModel : Ses
    {
        public virtual SesMail Mail { get; set; }
        public virtual SesDeliveryEvent Delivery { get; set; }
    }
}
