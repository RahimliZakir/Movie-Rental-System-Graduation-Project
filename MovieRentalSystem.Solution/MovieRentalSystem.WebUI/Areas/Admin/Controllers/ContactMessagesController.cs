#nullable disable
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.ContactMessagesModule;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "contactmessages.config")]
    public class ContactMessagesController : Controller
    {
        readonly IMediator mediator;

        public ContactMessagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index(ContactMessagePagedQuery query)
        {
            query.Response = await mediator.Send(query);

            return View(query);
        }

        public async Task<IActionResult> Details(ContactMessageSingleQuery query)
        {
            ContactMessageViewModel message = await mediator.Send(query);

            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        async public Task<IActionResult> Answer(ContactMessageSingleQuery query)
        {
            ContactMessageViewModel notAnsweredMessage = await mediator.Send(query);

            AnswerContactMessageFormModel formModel = new()
            {
                Id = notAnsweredMessage.Id,
                Name = notAnsweredMessage.Name,
                Lastname = notAnsweredMessage.Lastname,
                EmailAddress = notAnsweredMessage.EmailAddress,
                ContactMessageType = notAnsweredMessage.ContactMessageType.Text,
                Content = notAnsweredMessage.Content,
                Answer = notAnsweredMessage.Answer
            };

            return View(formModel);
        }

        [HttpPost]
        async public Task<IActionResult> Answer(AnswerContactMessageCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ContactMessageRemoveCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
