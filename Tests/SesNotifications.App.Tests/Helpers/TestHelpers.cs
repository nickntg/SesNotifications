using System;
using SesNotifications.App.Models;

namespace SesNotifications.App.Tests.Helpers
{
    public class TestHelpers
    {
        public static SesOpenEventModel GetOpenEventModel(DateTime dt)
        {
            return new SesOpenEventModel
            {
                EventType = "Open",
                Mail = GetSesMail(dt),
                Open = new SesOpenEvent
                {
                    UserAgent = "user agent",
                    IpAddress = "ip_address",
                    Timestamp = dt.Iso8601()
                }
            };
        }

        public static SesDeliveryModel GetSesDeliveryModel(DateTime dt)
        {
            return new SesDeliveryModel
            {
                NotificationType = "Delivery",
                Mail = GetSesMail(dt),
                Delivery = new SesDelivery
                {
                    Recipients = new[] {"recipient_1"},
                    ReportingMta = "mta",
                    RemoteMtaIp = "mta_ip",
                    SmtpResponse = "response",
                    Timestamp = dt.Iso8601()
                }
            };
        }

        public static SesDeliveryEventModel GetSesDeliveryEventModel(DateTime dt)
        {
            return new SesDeliveryEventModel
            {
                EventType = "Delivery",
                Mail = GetSesMail(dt),
                Delivery = new SesDeliveryEvent
                {
                    Recipients = new[] { "recipient_1" },
                    ReportingMta = "mta",
                    SmtpResponse = "response",
                    Timestamp = dt.Iso8601()
                }
            };
        }

        public static SesComplaintModel GetSesComplaintModel(DateTime dt)
        {
            return new SesComplaintModel
            {
                NotificationType = "Complaint",
                Mail = GetSesMail(dt),
                Complaint = new SesComplaint
                {
                    Timestamp = dt.Iso8601(),
                    ComplainedRecipients = new[] {new SesComplaintRecipient {EmailAddress = "address"}},
                    FeedbackId = "feedback_id",
                    ComplaintFeedbackType = null,
                    ComplaintSubType = "abuse",
                    ArrivalDate = null,
                    UserAgent = null
                }
            };
        }

        public static SesComplaintEventModel GetSesComplaintEventModel(DateTime dt)
        {
            return new SesComplaintEventModel
            {
                NotificationType = "Complaint",
                Mail = GetSesMail(dt),
                Complaint = new SesComplaintEvent
                {
                    Timestamp = dt.Iso8601(),
                    ComplainedRecipients = new[] { new SesComplaintRecipient { EmailAddress = "address" } },
                    FeedbackId = "feedback_id",
                    ComplaintFeedbackType = null,
                    ComplaintSubType = "abuse",
                    ArrivalDate = null
                }
            };
        }
        public static SesBounceModel GetSesBounceModel(DateTime dt)
        {
            return new SesBounceModel
            {
                NotificationType = "Bounce",
                Mail = GetSesMail(dt),
                Bounce = new SesBounce
                {
                    BouncedRecipients = new [] {new SesBouncedRecipient {EmailAddress = "address"}},
                    ReportingMta = null,
                    BounceSubType = "bounce_sub_type",
                    Timestamp = dt.Iso8601(),
                    FeedbackId = "feedback_id",
                    BounceType = "bounce_type",
                    RemoteMtaIp = null
                }
            };
        }

        public static SesBounceEventModel GetSesBounceEventModel(DateTime dt)
        {
            return new SesBounceEventModel
            {
                NotificationType = "Bounce",
                Mail = GetSesMail(dt),
                Bounce = new SesBounceEvent
                {
                    BouncedRecipients = new[] { new SesBouncedRecipient { EmailAddress = "address" } },
                    ReportingMta = null,
                    BounceSubType = "bounce_sub_type",
                    Timestamp = dt.Iso8601(),
                    FeedbackId = "feedback_id",
                    BounceType = "bounce_type"
                }
            };
        }

        public static SesOpenEventModel GetSesOpenModel(DateTime dt)
        {
            return new SesOpenEventModel
            {
                EventType = "Open",
                Mail = GetSesMail(dt),
                Open = new SesOpenEvent
                {
                    UserAgent = "user_agent",
                    IpAddress = "ip_address",
                    Timestamp = dt.Iso8601()
                }
            };
        }

        public static SesSendEventModel GetSesSendModel(DateTime dt)
        {
            return new SesSendEventModel
            {
                EventType = "Send",
                Mail = GetSesMail(dt),
                Send = new SesSendEvent()
            };
        }

        public static SesMail GetSesMail(DateTime dt)
        {
            return new SesMail
            {
                SourceArn = "source_arn",
                MessageId = "message_id",
                SendingAccountId = "sending_account_id",
                Source = "source",
                SourceIp = "source_ip",
                Destination = new[] {"destination_1"},
                Timestamp = dt.Iso8601()
            };
        }
    }
}
