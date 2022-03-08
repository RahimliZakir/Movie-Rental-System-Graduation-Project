#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.AppInfosModule;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppInfosController : Controller
    {
        readonly IMediator mediator;

        public AppInfosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AppInfo> appInfos = await mediator.Send(new AppInfoGetAllActiveQuery());

            return View(appInfos);
        }

        public async Task<IActionResult> Details(AppInfoSingleQuery query)
        {
            AppInfoViewModel appInfo = await mediator.Send(query);

            if (appInfo == null)
            {
                return NotFound();
            }

            return View(appInfo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Map,HeaderContent,FooterContent,Description,FacebookLink,TwitterLink,LinkedinLink,TelegramLink")] AppInfoCreateCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        public async Task<IActionResult> Edit(AppInfoSingleQuery query)
        {
            AppInfoViewModel appInfo = await mediator.Send(query);

            if (appInfo == null)
            {
                return NotFound();
            }

            return View(appInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Map,HeaderContent,FooterContent,Description,FacebookLink,TwitterLink,LinkedinLink,TelegramLink,Id")] AppInfoEditCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AppInfoRemoveCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
