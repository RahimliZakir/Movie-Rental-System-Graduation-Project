using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.PersonalSideModule;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonalSideController : Controller
    {
        readonly IMediator mediator;

        public PersonalSideController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IActionResult> Index()
        {
            PersonalSideFormModel currentUser = await mediator.Send(new PersonalSideGetUserProfileQuery());

            return View(currentUser);
        }

        [Route("edit-profile-info")]
        async public Task<IActionResult> Edit()
        {
            PersonalSideFormModel currentUser = await mediator.Send(new PersonalSideGetUserProfileQuery());

            return View(currentUser);
        }

        [HttpPost("edit-profile-info")]
        async public Task<IActionResult> Edit(PersonalSideEditCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
