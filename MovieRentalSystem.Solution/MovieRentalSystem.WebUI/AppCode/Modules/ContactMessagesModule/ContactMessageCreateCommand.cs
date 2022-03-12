using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ContactMessagesModule
{
    public class ContactMessageCreateCommand : ContactMessageViewModel, IRequest<CommandJsonResponse>
    {
        public class ContactMessageCreateCommandHandler : IRequestHandler<ContactMessageCreateCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public ContactMessageCreateCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
            }

            public async Task<CommandJsonResponse> Handle(ContactMessageCreateCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (!ctx.IsValid())
                {
                    response.Error = true;
                    response.Message = "Məlumat düzgün göndərilməyib!";
                }

                if (ctx.IsValid())
                {
                    ContactMessage message = mapper.Map<ContactMessage>(request);

                    await db.ContactMessages.AddAsync(message, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "İsmarıcınız uğurla bizə göndərildi, sualınız cavablanan kimi Email ünvanınıza göndəriləcək!";
                }

                return response;
            }
        }
    }
}
