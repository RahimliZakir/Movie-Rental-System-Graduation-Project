using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.AutoMapper.Dtos;
using MovieRentalSystem.WebUI.AppCode.Modules.PersonalSideModule;

namespace MovieRentalSystem.WebUI.AppCode.Components
{
    public class LayoutUserViewComponent : ViewComponent
    {
        async public Task<IViewComponentResult> InvokeAsync()
        {
            using (IServiceScope scope = HttpContext.RequestServices.CreateScope())
            {
                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                LayoutUserDto userDto = await mediator.Send(new LayoutUserQuery());

                return View(userDto);
            }
        }
    }
}
