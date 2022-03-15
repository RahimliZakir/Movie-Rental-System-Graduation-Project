using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ContactMessageTypesModule
{
    public class ContactMessageTypeRemoveCommand : IRequest<CommandJsonResponse>
    {
        public int? Id { get; set; }

        public class ContactMessageTypeRemoveCommandHandler : IRequestHandler<ContactMessageTypeRemoveCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public ContactMessageTypeRemoveCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(ContactMessageTypeRemoveCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";

                    goto end;
                }

                ContactMessageType type = await db.ContactMessageType.FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                if (type == null)
                {
                    response.Error = true;
                    response.Message = "Məlumat mövcud deyil!";

                    goto end;
                }

                type.DeletedDate = DateTime.UtcNow.AddHours(4);
                type.DeletedByUserId = ctx.GetUserId();
                await db.SaveChangesAsync(cancellationToken);

                response.Error = false;
                response.Message = "Məlumat uğurla silindi!";

            end:
                return response;
            }
        }
    }
}
