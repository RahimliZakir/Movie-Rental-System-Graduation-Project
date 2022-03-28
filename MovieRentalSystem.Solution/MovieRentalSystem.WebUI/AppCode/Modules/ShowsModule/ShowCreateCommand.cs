using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule
{
    public class ShowCreateCommand : ShowViewModel, IRequest<CommandJsonResponse>
    {
        public class ShowCreateCommandHandler : IRequestHandler<ShowCreateCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;
            readonly IHostEnvironment env;

            public ShowCreateCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper, IHostEnvironment env)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
                this.env = env;
            }

            public async Task<CommandJsonResponse> Handle(ShowCreateCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.File == null)
                {
                    response.Error = true;
                    response.Message = "Xahiş olunur şəkil seçin!";
                    goto end;
                }

                if (!ctx.IsValid())
                {
                    response.Error = true;
                    response.Message = "Məlumat düzgün göndərilməyib!";
                }

                if (ctx.IsValid())
                {
                    string ext = Path.GetExtension(request.File.FileName);
                    string fileName = $"show-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                    string fullPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "shows", fileName);

                    using (FileStream fs = new(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        await request.File.CopyToAsync(fs, cancellationToken);
                    }

                    request.ImagePath = fileName;

                    Show show = mapper.Map<Show>(request);
                    show.CreatedByUserId = ctx.GetUserId();

                    await db.Shows.AddAsync(show, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Məlumat uğurla əlavə olundu!";
                }

            end:
                return response;
            }
        }
    }
}
