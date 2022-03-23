using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.BlogModule
{
    public class BlogCreateCommand : BlogViewModel, IRequest<CommandJsonResponse>
    {
        public class BlogCreateCommandHandler : IRequestHandler<BlogCreateCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;
            readonly IHostEnvironment env;

            public BlogCreateCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper, IHostEnvironment env)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
                this.env = env;
            }

            public async Task<CommandJsonResponse> Handle(BlogCreateCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Files == null || !request.Files.Any())
                {
                    ctx.AddModelError("", "Şəkil seçilməyib!");
                }

                if (!ctx.IsValid())
                {
                    response.Error = true;
                    response.Message = "Məlumat düzgün göndərilməyib!";
                }

                if (ctx.IsValid())
                {

                    if (request.Files != null && request.Files.Any())
                    {
                        request.BlogImages = new List<BlogImage>();

                        foreach (ImageItem item in request.Files)
                        {
                            string ext = Path.GetExtension(item.File.FileName);
                            string filename = $"blog-{Guid.NewGuid().ToString().Replace("-", "")}{ext}".ToLower();
                            string fullname = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "blogs", filename);

                            using (FileStream fs = new(fullname, FileMode.Create, FileAccess.Write))
                            {
                                await item.File.CopyToAsync(fs, cancellationToken);
                            }

                            request.BlogImages.Add(new BlogImage
                            {
                                ImagePath = filename,
                                IsMain = item.IsMain
                            });
                        }
                    }

                    try
                    {
                        Blog blog = mapper.Map<Blog>(request);
                        blog.CreatedByUserId = ctx.GetUserId();

                        await db.Blogs.AddAsync(blog, cancellationToken);
                        await db.SaveChangesAsync(cancellationToken);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    response.Error = false;
                    response.Message = "Məlumat uğurla əlavə olundu!";
                }

                return response;
            }
        }
    }
}
