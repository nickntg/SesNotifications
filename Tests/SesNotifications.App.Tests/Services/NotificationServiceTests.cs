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
                null, null, mockLogger.Object);

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
                null, null, mockLogger.Object);

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
                null, null, mockLogger.Object);

            service.HandleNotification(Complaint);

            mockNotifications.Verify(x => x.Save(It.IsAny<SesNotification>()), Times.Exactly(1));
            mockSesComplaints.Verify(x => x.Save(It.IsAny<SesComplaint>()), Times.Exactly(1));
        }

        [Fact]
        public void VerifyOpens()
        {
            var mockNotifications = new Mock<INotificationsRepository>(MockBehavior.Strict);
            var mockSesOpens = new Mock<ISesOpensEventsRepository>(MockBehavior.Strict);
            var mockLogger = new Mock<ILogger<NotificationService>>(MockBehavior.Loose);

            mockNotifications.Setup(x => x.Save(It.IsAny<SesNotification>()));
            mockSesOpens.Setup(x => x.Save(It.IsAny<SesOpenEvent>()));

            var service = new NotificationService(mockNotifications.Object, null, null, null,
                mockSesOpens.Object, null, mockLogger.Object);

            service.HandleNotification(Open);

            mockNotifications.Verify(x => x.Save(It.IsAny<SesNotification>()), Times.Exactly(1));
            mockSesOpens.Verify(x => x.Save(It.IsAny<SesOpenEvent>()), Times.Exactly(1));
        }

        [Fact]
        public void VerifySends()
        {
            var mockNotifications = new Mock<INotificationsRepository>(MockBehavior.Strict);
            var mockSesSends = new Mock<ISesSendEventsRepository>(MockBehavior.Strict);
            var mockLogger = new Mock<ILogger<NotificationService>>(MockBehavior.Loose);

            mockNotifications.Setup(x => x.Save(It.IsAny<SesNotification>()));
            mockSesSends.Setup(x => x.Save(It.IsAny<SesSendEvent>()));

            var service = new NotificationService(mockNotifications.Object, null, null, null,
                null, mockSesSends.Object,mockLogger.Object);

            service.HandleNotification(Send);

            mockNotifications.Verify(x => x.Save(It.IsAny<SesNotification>()), Times.Exactly(1));
            mockSesSends.Verify(x => x.Save(It.IsAny<SesSendEvent>()), Times.Exactly(1));
        }

        [Fact]
        public void VerifyInvalidException()
        {
            var mockLogger = new Mock<ILogger<NotificationService>>(MockBehavior.Loose);

            var service = new NotificationService(null, null, null, null, null, null, mockLogger.Object);

            Assert.Throws<JsonReaderException>(() => service.HandleNotification(NotJson));
        }

        [Fact]
        public void VerifyUnsupportedException()
        {
            var mockLogger = new Mock<ILogger<NotificationService>>(MockBehavior.Loose);

            var service = new NotificationService(null, null, null, null, null, null, mockLogger.Object);

            Assert.Throws<NotSupportedException>(() => service.HandleNotification(Invalid));
        }

        // Some of the examples taken from https://docs.aws.amazon.com/ses/latest/DeveloperGuide/notification-examples.html.
        private const string Delivery = "{      \"notificationType\":\"Delivery\",      \"mail\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"messageId\":\"0000014644fe5ef6-9a483358-9170-4cb4-a269-f5dcdf415321-000000\",         \"source\":\"john@example.com\",         \"sourceArn\": \"arn:aws:ses:us-west-2:888888888888:identity/example.com\",         \"sourceIp\": \"127.0.3.0\",         \"sendingAccountId\":\"123456789012\",         \"destination\":[            \"jane@example.com\"         ],           \"headersTruncated\":false,          \"headers\":[            {               \"name\":\"From\",              \"value\":\"\\\"John Doe\\\" <john@example.com>\"           },           {               \"name\":\"To\",              \"value\":\"\\\"Jane Doe\\\" <jane@example.com>\"           },           {               \"name\":\"Message-ID\",              \"value\":\"custom-message-ID\"           },           {               \"name\":\"Subject\",              \"value\":\"Hello\"           },           {               \"name\":\"Content-Type\",              \"value\":\"text/plain; charset=\\\"UTF-8\\\"\"           },           {               \"name\":\"Content-Transfer-Encoding\",              \"value\":\"base64\"           },           {               \"name\":\"Date\",              \"value\":\"Wed, 27 Jan 2016 14:58:45 +0000\"           }          ],          \"commonHeaders\":{             \"from\":[                \"John Doe <john@example.com>\"            ],            \"date\":\"Wed, 27 Jan 2016 14:58:45 +0000\",            \"to\":[                \"Jane Doe <jane@example.com>\"            ],            \"messageId\":\"custom-message-ID\",            \"subject\":\"Hello\"          }       },      \"delivery\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"recipients\":[\"jane@example.com\"],         \"processingTimeMillis\":546,              \"reportingMTA\":\"a8-70.smtp-out.amazonses.com\",         \"smtpResponse\":\"250 ok:  Message 64111812 accepted\",         \"remoteMtaIp\":\"127.0.2.0\"      }    }";
        private const string Complaint ="{      \"notificationType\":\"Complaint\",      \"complaint\":{         \"userAgent\":\"AnyCompany Feedback Loop (V0.01)\",         \"complainedRecipients\":[            {               \"emailAddress\":\"richard@example.com\"            }         ],         \"complaintFeedbackType\":\"abuse\",         \"arrivalDate\":\"2016-01-27T14:59:38.237Z\",         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"feedbackId\":\"000001378603177f-18c07c78-fa81-4a58-9dd1-fedc3cb8f49a-000000\"      },      \"mail\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"messageId\":\"000001378603177f-7a5433e7-8edb-42ae-af10-f0181f34d6ee-000000\",         \"source\":\"john@example.com\",         \"sourceArn\": \"arn:aws:ses:us-west-2:888888888888:identity/example.com\",         \"sourceIp\": \"127.0.3.0\",         \"sendingAccountId\":\"123456789012\",         \"destination\":[            \"jane@example.com\",            \"mary@example.com\",            \"richard@example.com\"         ],           \"headersTruncated\":false,          \"headers\":[            {              \"name\":\"From\",             \"value\":\"\\\"John Doe\\\" <john@example.com>\"           },           {              \"name\":\"To\",             \"value\":\"\\\"Jane Doe\\\" <jane@example.com>, \\\"Mary Doe\\\" <mary@example.com>, \\\"Richard Doe\\\" <richard@example.com>\"           },           {              \"name\":\"Message-ID\",             \"value\":\"custom-message-ID\"           },           {              \"name\":\"Subject\",             \"value\":\"Hello\"           },           {              \"name\":\"Content-Type\",             \"value\":\"text/plain; charset=\\\"UTF-8\\\"\"           },           {              \"name\":\"Content-Transfer-Encoding\",             \"value\":\"base64\"           },           {              \"name\":\"Date\",             \"value\":\"Wed, 27 Jan 2016 14:05:45 +0000\"           }         ],         \"commonHeaders\":{            \"from\":[               \"John Doe <john@example.com>\"           ],           \"date\":\"Wed, 27 Jan 2016 14:05:45 +0000\",           \"to\":[               \"Jane Doe <jane@example.com>, Mary Doe <mary@example.com>, Richard Doe <richard@example.com>\"           ],           \"messageId\":\"custom-message-ID\",           \"subject\":\"Hello\"         }      }   }";
        private const string Bounce = "{      \"notificationType\":\"Bounce\",      \"bounce\":{         \"bounceType\":\"Permanent\",         \"bounceSubType\": \"General\",         \"bouncedRecipients\":[            {               \"emailAddress\":\"jane@example.com\"            },            {               \"emailAddress\":\"richard@example.com\"            }         ],         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"feedbackId\":\"00000137860315fd-869464a4-8680-4114-98d3-716fe35851f9-000000\",         \"remoteMtaIp\":\"127.0.2.0\"      },      \"mail\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"messageId\":\"00000137860315fd-34208509-5b74-41f3-95c5-22c1edc3c924-000000\",         \"source\":\"john@example.com\",         \"sourceArn\": \"arn:aws:ses:us-west-2:888888888888:identity/example.com\",         \"sourceIp\": \"127.0.3.0\",         \"sendingAccountId\":\"123456789012\",         \"destination\":[            \"jane@example.com\",            \"mary@example.com\",            \"richard@example.com\"         ],        \"headersTruncated\":false,        \"headers\":[          {             \"name\":\"From\",            \"value\":\"\\\"John Doe\\\" <john@example.com>\"         },         {             \"name\":\"To\",            \"value\":\"\\\"Jane Doe\\\" <jane@example.com>, \\\"Mary Doe\\\" <mary@example.com>, \\\"Richard Doe\\\" <richard@example.com>\"         },         {             \"name\":\"Message-ID\",            \"value\":\"custom-message-ID\"         },         {             \"name\":\"Subject\",            \"value\":\"Hello\"         },         {             \"name\":\"Content-Type\",            \"value\":\"text/plain; charset=\\\"UTF-8\\\"\"         },         {             \"name\":\"Content-Transfer-Encoding\",            \"value\":\"base64\"         },         {             \"name\":\"Date\",            \"value\":\"Wed, 27 Jan 2016 14:05:45 +0000\"          }         ],         \"commonHeaders\":{            \"from\":[               \"John Doe <john@example.com>\"           ],           \"date\":\"Wed, 27 Jan 2016 14:05:45 +0000\",           \"to\":[               \"Jane Doe <jane@example.com>, Mary Doe <mary@example.com>, Richard Doe <richard@example.com>\"           ],           \"messageId\":\"custom-message-ID\",           \"subject\":\"Hello\"         }      }  }";
        private const string Open = "{\"eventType\": \"Open\",\"mail\": {\"timestamp\": \"2020-05-08T08:42:19.146Z\",\"source\": \"d.test+python@test.gr\",\"sendingAccountId\": \"257120724488\",\"messageId\": \"01070171f3731a8a-8ab938e9-787d-4165-b967-fdb2abec1fc7-000000\",\"destination\": [\"mr.test@gmail.com\"],\"headersTruncated\": false,\"headers\": [{\"name\": \"Received\",\"value\": \"from [127.0.1.1] ([11.111.111.68]) by email-smtp.amazonaws.com with SMTP (SimpleEmailService-d-GG6TMY8U3) id 4E25Kp3rgG6djvRHUmZH for mr.test@gmail.com; Fri, 08 May 2020 08:42:19 +0000 (UTC)\"},{\"name\": \"Content-Type\",\"value\": \"multipart/alternative\"},{\"name\": \"MIME-Version\",\"value\": \"1.0\"},{\"name\": \"Subject\",\"value\": \"Amazon SES Test (Python smtplib) staging\"},{\"name\": \"From\",\"value\": \"Dimos Python script <d.test+python@test.gr>\"},{\"name\": \"To\",\"value\": \"mr.test@gmail.com\"},{\"name\": \"X-SES-CONFIGURATION-SET\",\"value\": \"ses-to-sqs\"},{\"name\": \"Message-ID\",\"value\": \"null\"}],\"commonHeaders\": {\"from\": [\"Dimos Python script <d.test+python@test.gr>\"],\"to\": [\"mr.test@gmail.com\"],\"messageId\": \"01070171f3731a8a-8ab938e9-787d-4165-b967-fdb2abec1fc7-000000\",\"subject\": \"Amazon SES Test (Python smtplib) staging\"},\"tags\": {\"ses:operation\": [\"SendSmtpEmail\"],\"ses:configuration-set\": [\"ses-to-sqs\"],\"ses:source-ip\": [\"11.111.112.61\"],\"ses:from-domain\": [\"test.gr\"],\"ses:caller-identity\": [\"ses-smtp-notificator\"]}},\"open\": {\"timestamp\": \"2020-05-08T08:56:46.917Z\",\"userAgent\": \"Mozilla/5.0 (Windows NT 5.1; rv:11.0) Gecko Firefox/11.0 (via ggpht.com GoogleImageProxy)\",\"ipAddress\": \"11.111.11.111\"}}";
        private const string Send = "{\"eventType\":\"Send\",\"mail\":{\"timestamp\":\"2020-05-29T15:26:26.221Z\",\"source\":\"tester+python@xe.gr\",\"sourceArn\":\"arn:aws:ses:eu-central-1:234567890:identity/test.com\",\"sendingAccountId\":\"234567890\",\"messageId\":\"01070172610aa1ad-ffae92b5-dd4d-42e5-aa72-e75d9cd4a995-000000\",\"destination\":[\"tester@gmail.com\"],\"headersTruncated\":false,\"headers\":[{\"name\":\"Received\",\"value\":\"from [127.0.1.1] (ppp-12-34-56-78.home.test.com [12.34.567.222]) by email-smtp.amazonaws.com with SMTP (SimpleEmailService-d-32P1K87U3) id f4qxpujfRBhaohG8QqoE for test@gmail.com; Fri, 29 May 2020 15:26:26 +0000 (UTC)\"},{\"name\":\"Content-Type\",\"value\":\"multipart/alternative; boundary=1497643119139888747\"},{\"name\":\"MIME-Version\",\"value\":\"1.0\"},{\"name\":\"Subject\",\"value\":\"Amazon SES Test (Python smtplib) prod with configset\"},{\"name\":\"From\",\"value\":\"Python script <test+python@test.com>\"},{\"name\":\"To\",\"value\":\"tester@gmail.com\"},{\"name\":\"X-SES-CONFIGURATION-SET\",\"value\":\"ses-events\"}],\"commonHeaders\":{\"from\":[\"Python script <test+python@test.com>\"],\"to\":[\"tester@gmail.com\"],\"messageId\":\"01070172610aa1ad-ffae92b5-dd4d-42e5-aa72-e75d9cd4a995-000000\",\"subject\":\"Amazon SES Test (Python smtplib) prod with configset\"},\"tags\":{\"ses:operation\":[\"SendSmtpEmail\"],\"ses:configuration-set\":[\"ses-events\"],\"ses:source-ip\":[\"12.34.567.156\"],\"ses:from-domain\":[\"test.com\"],\"ses:caller-identity\":[\"ses-smtp-notificator\"]}},\"send\":{}}";
        private const string Invalid = "{      \"notificationType\":\"SomethingElse\",      \"mail\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"messageId\":\"0000014644fe5ef6-9a483358-9170-4cb4-a269-f5dcdf415321-000000\",         \"source\":\"john@example.com\",         \"sourceArn\": \"arn:aws:ses:us-west-2:888888888888:identity/example.com\",         \"sourceIp\": \"127.0.3.0\",         \"sendingAccountId\":\"123456789012\",         \"destination\":[            \"jane@example.com\"         ],           \"headersTruncated\":false,          \"headers\":[            {               \"name\":\"From\",              \"value\":\"\\\"John Doe\\\" <john@example.com>\"           },           {               \"name\":\"To\",              \"value\":\"\\\"Jane Doe\\\" <jane@example.com>\"           },           {               \"name\":\"Message-ID\",              \"value\":\"custom-message-ID\"           },           {               \"name\":\"Subject\",              \"value\":\"Hello\"           },           {               \"name\":\"Content-Type\",              \"value\":\"text/plain; charset=\\\"UTF-8\\\"\"           },           {               \"name\":\"Content-Transfer-Encoding\",              \"value\":\"base64\"           },           {               \"name\":\"Date\",              \"value\":\"Wed, 27 Jan 2016 14:58:45 +0000\"           }          ],          \"commonHeaders\":{             \"from\":[                \"John Doe <john@example.com>\"            ],            \"date\":\"Wed, 27 Jan 2016 14:58:45 +0000\",            \"to\":[                \"Jane Doe <jane@example.com>\"            ],            \"messageId\":\"custom-message-ID\",            \"subject\":\"Hello\"          }       },      \"delivery\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"recipients\":[\"jane@example.com\"],         \"processingTimeMillis\":546,              \"reportingMTA\":\"a8-70.smtp-out.amazonses.com\",         \"smtpResponse\":\"250 ok:  Message 64111812 accepted\",         \"remoteMtaIp\":\"127.0.2.0\"      }    }";
        private const string NotJson = "some string";
    }
}
