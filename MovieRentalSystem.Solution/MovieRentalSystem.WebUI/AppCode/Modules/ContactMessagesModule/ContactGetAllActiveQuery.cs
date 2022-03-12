using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ContactMessagesModule
{
    public class ContactMessageGetAllActiveQuery : IRequest<IEnumerable<ContactMessage>>
    {
        public class ContactMessageGetAllActiveQueryHandler : IRequestHandler<ContactMessageGetAllActiveQuery, IEnumerable<ContactMessage>>
        {
            readonly MovieDbContext db;

            public ContactMessageGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<ContactMessage>> Handle(ContactMessageGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<ContactMessage> messages = await db.ContactMessages.Where(g => g.DeletedDate == null).ToListAsync(cancellationToken);

                return messages;
            }
        }
    }
}
