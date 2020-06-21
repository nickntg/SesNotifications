using SesNotifications.App.Helpers;
using Xunit;

namespace SesNotifications.App.Tests.Helpers
{
    public class MatchingHelperTests
    {
        [Fact]
        public void FindMatch()
        {
            var t = Delivery.TokenizeJson();

            var found = t.FindToken("$.notificationType");

            Assert.NotNull(found);
            Assert.Equal("Delivery", found.ToString());
        }

        [Fact]
        public void DoNotFindMatch()
        {
            var t = Delivery.TokenizeJson();

            var found = t.FindToken("$.nothing");

            Assert.Null(found);
        }

        [Theory]
        [InlineData("john@example.com", true)]
        [InlineData("other@example.com", false)]
        public void Regex(string toFind, bool expected)
        {
            var t = Delivery.TokenizeJson();

            var token = t.FindToken("$.mail.source");

            var found = token.ToString().IsMatch(toFind);

            Assert.Equal(expected, found);
        }

        private const string Delivery = "{      \"notificationType\":\"Delivery\",      \"mail\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"messageId\":\"0000014644fe5ef6-9a483358-9170-4cb4-a269-f5dcdf415321-000000\",         \"source\":\"john@example.com\",         \"sourceArn\": \"arn:aws:ses:us-west-2:888888888888:identity/example.com\",         \"sourceIp\": \"127.0.3.0\",         \"sendingAccountId\":\"123456789012\",         \"destination\":[            \"jane@example.com\"         ],           \"headersTruncated\":false,          \"headers\":[            {               \"name\":\"From\",              \"value\":\"\\\"John Doe\\\" <john@example.com>\"           },           {               \"name\":\"To\",              \"value\":\"\\\"Jane Doe\\\" <jane@example.com>\"           },           {               \"name\":\"Message-ID\",              \"value\":\"custom-message-ID\"           },           {               \"name\":\"Subject\",              \"value\":\"Hello\"           },           {               \"name\":\"Content-Type\",              \"value\":\"text/plain; charset=\\\"UTF-8\\\"\"           },           {               \"name\":\"Content-Transfer-Encoding\",              \"value\":\"base64\"           },           {               \"name\":\"Date\",              \"value\":\"Wed, 27 Jan 2016 14:58:45 +0000\"           }          ],          \"commonHeaders\":{             \"from\":[                \"John Doe <john@example.com>\"            ],            \"date\":\"Wed, 27 Jan 2016 14:58:45 +0000\",            \"to\":[                \"Jane Doe <jane@example.com>\"            ],            \"messageId\":\"custom-message-ID\",            \"subject\":\"Hello\"          }       },      \"delivery\":{         \"timestamp\":\"2016-01-27T14:59:38.237Z\",         \"recipients\":[\"jane@example.com\"],         \"processingTimeMillis\":546,              \"reportingMTA\":\"a8-70.smtp-out.amazonses.com\",         \"smtpResponse\":\"250 ok:  Message 64111812 accepted\",         \"remoteMtaIp\":\"127.0.2.0\"      }    }";
    }
}
