using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.DirectorsModule
{
    public class DirectorEditCommand : DirectorViewModel, IRequest<CommandJsonResponse>
    {
        public class DirectorEditCommandHandler : IRequestHandler<DirectorEditCommand, CommandJsonResponse>
        {
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public DirectorEditCommandHandler(IActionContextAccessor ctx, IMapper mapper)
            {
                this.ctx = ctx;
                this.mapper = mapper;
            }

            public async Task<CommandJsonResponse> Handle(DirectorEditCommand request, CancellationToken cancellationToken)
            {
                using (IServiceScope scope = ctx.ActionContext.HttpContext.RequestServices.CreateScope())
                {
                    MovieDbContext db = scope.ServiceProvider.GetRequiredService<MovieDbContext>();

                    CommandJsonResponse response = new();

                    if (request.Id == null || request.Id <= 0)
                    {
                        response.Error = true;
                        response.Message = "Məlumatın tamlığı qorunmayıb!";
                        goto end;
                    }

                    Director entity = await db.Directors.AsNoTracking().FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

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
                            request.CreatedByUserId = entity.CreatedByUserId;
                            Director director = mapper.Map(request, entity);

                            db.Directors.Update(director);
                            await db.SaveChangesAsync(cancellationToken);
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }

                        response.Error = false;
                        response.Message = "Məlumat uğurla yeniləndi!";
                    }

                end:
                    return response;
                }
            }
        }
    }
}
