using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ContactMessageTypesModule
{
    public class ContactMessageTypeEditCommand : ContactMessageTypeViewModel, IRequest<CommandJsonResponse>
    {
        public class ContactMessageTypeEditCommandHandler : IRequestHandler<ContactMessageTypeEditCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public ContactMessageTypeEditCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
            }

            public async Task<CommandJsonResponse> Handle(ContactMessageTypeEditCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";
                    goto end;
                }

                ContactMessageType entity = await db.ContactMessageType.FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

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
                    request.CreatedByUserId = entity.CreatedByUserId;
                    ContactMessageType type = mapper.Map(request, entity);

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
