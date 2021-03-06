using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.Modules.PersonalSideModule
{
    public class PersonalSideEditCommand : PersonalSideFormModel, IRequest<CommandJsonResponse>
    {
        public class PersonalSideEditCommandHandler : IRequestHandler<PersonalSideEditCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IHostEnvironment env;
            readonly IMapper mapper;

            public PersonalSideEditCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IHostEnvironment env, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.env = env;
                this.mapper = mapper;
            }

            async public Task<CommandJsonResponse> Handle(PersonalSideEditCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";
                }

                AppUser currentUser = await db.Users.FirstOrDefaultAsync(u => u.Id == request.Id);

                if (currentUser == null)
                {
                    response.Error = true;
                    response.Message = "Məlumat mövcud deyil!";
                }

                if (ctx.IsValid())
                {
                    string fullPath = null;
                    string currentPath = null;

                    if (currentUser.ImagePath != null || (currentUser.ImagePath == null && request.File != null))
                    {
                        if (request.File == null && !string.IsNullOrWhiteSpace(request.FileTemp))
                        {
                            request.ImagePath = currentUser.ImagePath;
                        }
                        else if (request.File == null)
                        {
                            currentPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "personalside", currentUser.ImagePath);
                        }
                        else if (request.File != null)
                        {
                            string ext = Path.GetExtension(request.File.FileName);
                            string fileName = $"user-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                            fullPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "personalside", fileName);

                            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                            {
                                await request.File.CopyToAsync(fs, cancellationToken);
                            }

                            request.ImagePath = fileName;
                        }
                    }
                    request.EmailConfirmed = currentUser.EmailConfirmed;
                    AppUser user = mapper.Map(request, currentUser);

                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Məlumatlar uğurla yadda saxlanıldı!";
                }

                return response;
            }
        }
    }
}
