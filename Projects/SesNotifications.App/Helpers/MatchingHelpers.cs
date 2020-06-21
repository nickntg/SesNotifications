using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace SesNotifications.App.Helpers
{
    public static class MatchingHelpers
    {
        public static JToken TokenizeJson(this string json)
        {
            return JToken.Parse(json);
        }

        public static JToken FindToken(this JToken token, string jsonMatcher)
        {
            return token.SelectToken(jsonMatcher);
        }

        public static bool IsMatch(this string value, string regex)
        {
            return new Regex(regex).IsMatch(value);
        }
    }
}