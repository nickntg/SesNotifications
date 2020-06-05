namespace SesNotifications.App.Models
{
    public class SesComplaintEventModel : Ses
    {
        public virtual SesMail Mail { get; set; }
        public virtual SesComplaintEvent Complaint { get; set; }
    }
}