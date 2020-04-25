using System;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using SesNotifications.App.Services;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;
using Xunit;

namespace SesNotifications.App.Tests.Services
{
    public class NotificationServiceTests
    {
        [Fact]
        public void VerifyDelivery()
        {
            var mockNotifications = new Mock<INotificationsRepository>(MockBehavior.Strict);
            var mockSesDeliveries = new Mock<ISesDeliveriesRepository>(MockBehavior.Strict);
            var mockLogger = new Mock<ILogger<NotificationService>>(MockBehavior.Loose);

            mockNotifications.Setup(x => x.Save(It.IsAny<SesNotification>()));
            mockSesDeliveries.Setup(x => x.Save(It.IsAny<SesDelivery>()));

            var service = new NotificationService(mockNotifications.Object, null, null, mockSesDeliveries.Object,
                mockLogger.Object);

            service.HandleNotification(Delivery);

            mockNotifications.Verify(x => x.Save(It.IsAny<SesNotification>()), Times.Exactly(1));
            mockSesDeliveries.Verify(x => x.Save(It.IsAny<SesDelivery>()), Times.Exactly(1));
        }

        [Fact]
        public void VerifyBounce()
        {
            var mockNotifications = new Mock<INotificationsRepository>(MockBehavior.Strict);
            var mockSesBounces = new Mock<ISesBouncesRepository>(MockBehavior.Strict);
            var mockLogger = new Mock<ILogger<NotificationService>>(MockBehavior.Loose);

            mockNotifications.Setup(x => x.Save(It.IsAny<SesNotification>()));
            mockSesBounces.Setup(x => x.Save(It.IsAny<SesBounce>()));

            var service = new NotificationService(mockNotifications.Object, mockSesBounces.Object, null, null,
                mockLogger.Object);

            service.HandleNotification(Bounce);

            mockNotifications.Verify(x => x.Save(It.IsAny<SesNotification>()), Times.Exactly(1));
            mockSesBounces.Verify(x => x.Save(It.IsAny<SesBounce>()), Times.Exactly(1));
        }

        [Fact]
        public void VerifyComplaints()
        {
            var mockNotifications = new Mock<INotificationsRepository>(MockBehavior.Strict);
            var mockSesComplaints = new Mock<ISesComplaintsRepository>(MockBehavior.Strict);
            var mockLogger = new Mock<ILogger<NotificationService>>(MockBehavior.Loose);

            mockNotifications.Setup(x => x.Save(It.IsAny<SesNotification>()));
            mockSesComplaints.Setup(x => x.Save(It.IsAny<SesComplaint>()));

            var service = new NotificationService(mockNotifications.Object, null, mockSesComplaints.Object, null,
                mockLogger.Object);

            service.HandleNotification(Complaint);

            mockNotifications.Verify(x => x.Save(It.IsAny<SesNotification>()), Times.Exactly(1));
            mockSesComplaints.Verify(x => x.Save(It.IsAny<SesComplaint>()), Times.Exactly(1));
        }

        [Fact]
        public void VerifyInvalidException()
        {
            var mockLogger = new Mock<ILogger<NotificationService>>(MockBehavior.Loose);

            var service = new NotificationService(null, null, null, null, mockLogger.Object);

            Assert.Throws<JsonReaderException>(() => service.HandleNotification(NotJson));
        }

        [Fact]
        public void VerifyUnsupportedException()
        {
            var mockLogger = new Mock<ILogger<NotificationService>>(MockBehavior.Loose);

            var service = new NotificationService(null, null, null, null, mockLogger.Object);

            Assert.Throws<NotSupportedException>(() => service.HandleNotification(Invalid));
        }

        // Examples taken from https://docs.aws.amazon.com/ses/latest/DeveloperGuide/notification-examples.html.
        private const string Delivery = "{      \"notificationType\":\"Delivery\",      \"mail\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"messageId\":\"0000014644fe5ef6-9a483358-9170-4cb4-a269-f5dcdf415321-000000\",         \"source\":\"john@example.com\",         \"sourceArn\": \"arn:aws:ses:us-west-2:888888888888:identity/example.com\",         \"sourceIp\": \"127.0.3.0\",         \"sendingAccountId\":\"123456789012\",         \"destination\":[            \"jane@example.com\"         ],           \"headersTruncated\":false,          \"headers\":[            {               \"name\":\"From\",              \"value\":\"\\\"John Doe\\\" <john@example.com>\"           },           {               \"name\":\"To\",              \"value\":\"\\\"Jane Doe\\\" <jane@example.com>\"           },           {               \"name\":\"Message-ID\",              \"value\":\"custom-message-ID\"           },           {               \"name\":\"Subject\",              \"value\":\"Hello\"           },           {               \"name\":\"Content-Type\",              \"value\":\"text/plain; charset=\\\"UTF-8\\\"\"           },           {               \"name\":\"Content-Transfer-Encoding\",              \"value\":\"base64\"           },           {               \"name\":\"Date\",              \"value\":\"Wed, 27 Jan 2016 14:58:45 +0000\"           }          ],          \"commonHeaders\":{             \"from\":[                \"John Doe <john@example.com>\"            ],            \"date\":\"Wed, 27 Jan 2016 14:58:45 +0000\",            \"to\":[                \"Jane Doe <jane@example.com>\"            ],            \"messageId\":\"custom-message-ID\",            \"subject\":\"Hello\"          }       },      \"delivery\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"recipients\":[\"jane@example.com\"],         \"processingTimeMillis\":546,              \"reportingMTA\":\"a8-70.smtp-out.amazonses.com\",         \"smtpResponse\":\"250 ok:  Message 64111812 accepted\",         \"remoteMtaIp\":\"127.0.2.0\"      }    }";
        private const string Complaint ="{      \"notificationType\":\"Complaint\",      \"complaint\":{         \"userAgent\":\"AnyCompany Feedback Loop (V0.01)\",         \"complainedRecipients\":[            {               \"emailAddress\":\"richard@example.com\"            }         ],         \"complaintFeedbackType\":\"abuse\",         \"arrivalDate\":\"2016-01-27T14:59:38.237Z\",         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"feedbackId\":\"000001378603177f-18c07c78-fa81-4a58-9dd1-fedc3cb8f49a-000000\"      },      \"mail\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"messageId\":\"000001378603177f-7a5433e7-8edb-42ae-af10-f0181f34d6ee-000000\",         \"source\":\"john@example.com\",         \"sourceArn\": \"arn:aws:ses:us-west-2:888888888888:identity/example.com\",         \"sourceIp\": \"127.0.3.0\",         \"sendingAccountId\":\"123456789012\",         \"destination\":[            \"jane@example.com\",            \"mary@example.com\",            \"richard@example.com\"         ],           \"headersTruncated\":false,          \"headers\":[            {              \"name\":\"From\",             \"value\":\"\\\"John Doe\\\" <john@example.com>\"           },           {              \"name\":\"To\",             \"value\":\"\\\"Jane Doe\\\" <jane@example.com>, \\\"Mary Doe\\\" <mary@example.com>, \\\"Richard Doe\\\" <richard@example.com>\"           },           {              \"name\":\"Message-ID\",             \"value\":\"custom-message-ID\"           },           {              \"name\":\"Subject\",             \"value\":\"Hello\"           },           {              \"name\":\"Content-Type\",             \"value\":\"text/plain; charset=\\\"UTF-8\\\"\"           },           {              \"name\":\"Content-Transfer-Encoding\",             \"value\":\"base64\"           },           {              \"name\":\"Date\",             \"value\":\"Wed, 27 Jan 2016 14:05:45 +0000\"           }         ],         \"commonHeaders\":{            \"from\":[               \"John Doe <john@example.com>\"           ],           \"date\":\"Wed, 27 Jan 2016 14:05:45 +0000\",           \"to\":[               \"Jane Doe <jane@example.com>, Mary Doe <mary@example.com>, Richard Doe <richard@example.com>\"           ],           \"messageId\":\"custom-message-ID\",           \"subject\":\"Hello\"         }      }   }";
        private const string Bounce = "{      \"notificationType\":\"Bounce\",      \"bounce\":{         \"bounceType\":\"Permanent\",         \"bounceSubType\": \"General\",         \"bouncedRecipients\":[            {               \"emailAddress\":\"jane@example.com\"            },            {               \"emailAddress\":\"richard@example.com\"            }         ],         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"feedbackId\":\"00000137860315fd-869464a4-8680-4114-98d3-716fe35851f9-000000\",         \"remoteMtaIp\":\"127.0.2.0\"      },      \"mail\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"messageId\":\"00000137860315fd-34208509-5b74-41f3-95c5-22c1edc3c924-000000\",         \"source\":\"john@example.com\",         \"sourceArn\": \"arn:aws:ses:us-west-2:888888888888:identity/example.com\",         \"sourceIp\": \"127.0.3.0\",         \"sendingAccountId\":\"123456789012\",         \"destination\":[            \"jane@example.com\",            \"mary@example.com\",            \"richard@example.com\"         ],        \"headersTruncated\":false,        \"headers\":[          {             \"name\":\"From\",            \"value\":\"\\\"John Doe\\\" <john@example.com>\"         },         {             \"name\":\"To\",            \"value\":\"\\\"Jane Doe\\\" <jane@example.com>, \\\"Mary Doe\\\" <mary@example.com>, \\\"Richard Doe\\\" <richard@example.com>\"         },         {             \"name\":\"Message-ID\",            \"value\":\"custom-message-ID\"         },         {             \"name\":\"Subject\",            \"value\":\"Hello\"         },         {             \"name\":\"Content-Type\",            \"value\":\"text/plain; charset=\\\"UTF-8\\\"\"         },         {             \"name\":\"Content-Transfer-Encoding\",            \"value\":\"base64\"         },         {             \"name\":\"Date\",            \"value\":\"Wed, 27 Jan 2016 14:05:45 +0000\"          }         ],         \"commonHeaders\":{            \"from\":[               \"John Doe <john@example.com>\"           ],           \"date\":\"Wed, 27 Jan 2016 14:05:45 +0000\",           \"to\":[               \"Jane Doe <jane@example.com>, Mary Doe <mary@example.com>, Richard Doe <richard@example.com>\"           ],           \"messageId\":\"custom-message-ID\",           \"subject\":\"Hello\"         }      }  }";
        private const string Invalid = "{      \"notificationType\":\"SomethingElse\",      \"mail\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"messageId\":\"0000014644fe5ef6-9a483358-9170-4cb4-a269-f5dcdf415321-000000\",         \"source\":\"john@example.com\",         \"sourceArn\": \"arn:aws:ses:us-west-2:888888888888:identity/example.com\",         \"sourceIp\": \"127.0.3.0\",         \"sendingAccountId\":\"123456789012\",         \"destination\":[            \"jane@example.com\"         ],           \"headersTruncated\":false,          \"headers\":[            {               \"name\":\"From\",              \"value\":\"\\\"John Doe\\\" <john@example.com>\"           },           {               \"name\":\"To\",              \"value\":\"\\\"Jane Doe\\\" <jane@example.com>\"           },           {               \"name\":\"Message-ID\",              \"value\":\"custom-message-ID\"           },           {               \"name\":\"Subject\",              \"value\":\"Hello\"           },           {               \"name\":\"Content-Type\",              \"value\":\"text/plain; charset=\\\"UTF-8\\\"\"           },           {               \"name\":\"Content-Transfer-Encoding\",              \"value\":\"base64\"           },           {               \"name\":\"Date\",              \"value\":\"Wed, 27 Jan 2016 14:58:45 +0000\"           }          ],          \"commonHeaders\":{             \"from\":[                \"John Doe <john@example.com>\"            ],            \"date\":\"Wed, 27 Jan 2016 14:58:45 +0000\",            \"to\":[                \"Jane Doe <jane@example.com>\"            ],            \"messageId\":\"custom-message-ID\",            \"subject\":\"Hello\"          }       },      \"delivery\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"recipients\":[\"jane@example.com\"],         \"processingTimeMillis\":546,              \"reportingMTA\":\"a8-70.smtp-out.amazonses.com\",         \"smtpResponse\":\"250 ok:  Message 64111812 accepted\",         \"remoteMtaIp\":\"127.0.2.0\"      }    }";
        private const string NotJson = "some string";
    }
}
