using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule
{
    public class ShowEditCommand : ShowViewModel, IRequest<CommandJsonResponse>
    {
        public class ShowEditCommandHandler : IRequestHandler<ShowEditCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;
            readonly IHostEnvironment env;

            public ShowEditCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper, IHostEnvironment env)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
                this.env = env;
            }

            public async Task<CommandJsonResponse> Handle(ShowEditCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";
                    goto end;
                }

                Show entity = await db.Shows.AsNoTracking().FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Məlumat mövcud deyil!";
                    goto end;
                }

                if (!ctx.IsValid())
                {
                    response.Error = true;
                    response.Message = "Məlumat düzgün göndərilməyib!";
                }

                if (ctx.IsValid())
                {
                    try
                    {
                        string currentPath = null;
                        string fullPath = null;

                        if (request.File == null && !string.IsNullOrWhiteSpace(request.FileTemp))
                        {
                            request.ImagePath = entity.ImagePath;
                        }
                        else if (request.File == null)
                        {
                            currentPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "shows", entity.ImagePath);
                        }
                        else if (request.File != null)
                        {
                            string ext = Path.GetExtension(request.File.FileName);
                            string fileName = $"show-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                            fullPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "shows", fileName);

                            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                            {
                                await request.File.CopyToAsync(fs, cancellationToken);
                            }

                            request.ImagePath = fileName;
                        }

                        request.CreatedByUserId = entity.CreatedByUserId;
                        Show show = mapper.Map(request, entity);

                        db.Shows.Update(show);
                        await db.SaveChangesAsync(cancellationToken);

                        response.Error = false;
                        response.Message = "Məlumat uğurla yeniləndi!";
                    }
                    catch (Exception ex)
                    {
                        response.Error = true;
                        response.Message = "Məlumat əlavə olunan zaman xəta baş verdi!";
                    }
                }

            end:
                return response;
            }
        }
    }
}
