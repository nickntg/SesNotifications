using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace SesNotifications.App.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl) =>
            new ChallengeResult(
                GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(LoginCallback), new {returnUrl})
                });

        public async Task<IActionResult> LoginCallback(string returnUrl)
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(Infrastructure.Constants.SignInScheme);

            if (!authenticateResult.Succeeded)
                return BadRequest();

            var claimsIdentity = new ClaimsIdentity(Infrastructure.Constants.ApplicationScheme);

            claimsIdentity.AddClaim(authenticateResult.Principal.FindFirst(ClaimTypes.Name));
            claimsIdentity.AddClaim(authenticateResult.Principal.FindFirst(ClaimTypes.Email));

            await HttpContext.SignInAsync(
                Infrastructure.Constants.ApplicationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                    {IsPersistent = true}); // IsPersistent will set a cookie that lasts for two weeks (by default).

            return LocalRedirect(returnUrl);
        }
    }
}