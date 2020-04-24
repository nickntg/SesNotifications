namespace SesNotifications.App.Models
{
    public class SesComplaintModel : Ses
    {
        public virtual SesMail Mail { get; set; }
        public virtual SesComplaint Complaint { get; set; }
    }
}