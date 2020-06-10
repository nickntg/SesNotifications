using SesNotifications.App.Helpers;
using Xunit;

namespace SesNotifications.App.Tests.Helpers
{
    public class StringHelperTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("value", "%value%")]
        public void VerifyLikePreparation(string email, string expected)
        {
            Assert.Equal(expected, email.PrepareForLike());
        }
    }
}