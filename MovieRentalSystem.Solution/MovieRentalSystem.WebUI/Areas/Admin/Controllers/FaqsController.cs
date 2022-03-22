#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.FaqsModule;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "faqs.config")]
    public class FaqsController : Controller
    {
        readonly IMediator mediator;

        public FaqsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index(FaqPagedQuery query)
        {
            query.Response = await mediator.Send(query);

            return View(query);
        }

        public async Task<IActionResult> Details(FaqSingleQuery query)
        {
            FaqViewModel faq = await mediator.Send(query);

            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Question,Answer")] FaqCreateCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        public async Task<IActionResult> Edit(FaqSingleQuery query)
        {
            FaqViewModel faq = await mediator.Send(query);

            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Question,Answer,Id")] FaqEditCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(FaqRemoveCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
