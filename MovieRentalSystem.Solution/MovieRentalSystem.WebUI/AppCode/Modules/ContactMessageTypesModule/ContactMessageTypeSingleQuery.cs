using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ContactMessageTypesModule
{
    public class ContactMessageTypeSingleQuery : IRequest<ContactMessageTypeViewModel>
    {
        public int? Id { get; set; }

        public class ContactMessageTypeSingleQueryHandler : IRequestHandler<ContactMessageTypeSingleQuery, ContactMessageTypeViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public ContactMessageTypeSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<ContactMessageTypeViewModel> Handle(ContactMessageTypeSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                ContactMessageType type = await db.ContactMessageType.FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                ContactMessageTypeViewModel viewModel = mapper.Map<ContactMessageTypeViewModel>(type);

                return viewModel;
            }
        }
    }
}
