using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.BlogModule
{
    public class BlogEditCommand : BlogViewModel, IRequest<CommandJsonResponse>
    {
        public class BlogEditCommandHandler : IRequestHandler<BlogEditCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;
            readonly IHostEnvironment env;

            public BlogEditCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper, IHostEnvironment env)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
                this.env = env;
            }

            public async Task<CommandJsonResponse> Handle(BlogEditCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";
                    goto end;
                }

                Blog entity = await db.Blogs
                                      .Include(b => b.BlogImages)
                                      .FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

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
                    foreach (ImageItem item in request.Files)
                    {
                        if (item.Id > 0 && item.Id != null)
                        {
                            BlogImage image = await db.BlogImages.FirstOrDefaultAsync(bi => bi.Id == item.Id, cancellationToken);

                            if (image != null)
                            {
                                if (string.IsNullOrWhiteSpace(item.TempPath))
                                {
                                    db.BlogImages.Remove(image);
                                }
                                else
                                {
                                    image.IsMain = item.IsMain;
                                }
                            }
                        }
                        else if (item.File != null)
                        {
                            string ext = Path.GetExtension(item.File.FileName);
                            string filename = $"blog-{Guid.NewGuid().ToString().Replace("-", "")}{ext}".ToLower();
                            string fullname = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "blogs", filename);

                            using (FileStream fs = new(fullname, FileMode.Create, FileAccess.Write))
                            {
                                await item.File.CopyToAsync(fs, cancellationToken);
                            }

                            entity.BlogImages.Add(new BlogImage
                            {
                                ImagePath = filename,
                                IsMain = item.IsMain
                            });
                        }
                    }

                    request.CreatedByUserId = entity.CreatedByUserId;
                    request.BlogImages = entity.BlogImages;
                    Blog blog = mapper.Map(request, entity);

                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Məlumat uğurla yeniləndi!";
                }

            end:
                return response;
            }
        }
    }
}
