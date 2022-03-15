using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        readonly SignInManager<AppUser> signInManager;

        public AccountController(SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        async public Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index), "Home", new
            {
                area = ""
            });
        }
    }
}
