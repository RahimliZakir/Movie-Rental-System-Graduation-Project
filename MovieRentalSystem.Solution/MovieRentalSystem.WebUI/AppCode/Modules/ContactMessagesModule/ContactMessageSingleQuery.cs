using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ContactMessagesModule
{
    public class ContactMessageSingleQuery : IRequest<ContactMessageViewModel>
    {
        public int? Id { get; set; }

        public class ContactMessageSingleQueryHandler : IRequestHandler<ContactMessageSingleQuery, ContactMessageViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public ContactMessageSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<ContactMessageViewModel> Handle(ContactMessageSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                ContactMessage message = await db.ContactMessages
                                                 .Include(c => c.ContactMessageType)
                                                 .FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                ContactMessageViewModel viewModel = mapper.Map<ContactMessageViewModel>(message);

                return viewModel;
            }
        }
    }
}
