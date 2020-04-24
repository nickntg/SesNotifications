namespace SesNotifications.App.Models
{
    public class SesDeliveryModel : Ses
    {
        public virtual SesMail Mail { get; set; }
        public virtual SesDelivery Delivery { get; set; }
    }
}
