using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ContactMessageTypesModule
{
    public class ContactMessageTypeGetAllActiveQuery : IRequest<IEnumerable<ContactMessageType>>
    {
        public class ContactMessageTypeGetAllActiveQueryHandler : IRequestHandler<ContactMessageTypeGetAllActiveQuery, IEnumerable<ContactMessageType>>
        {
            readonly MovieDbContext db;

            public ContactMessageTypeGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<ContactMessageType>> Handle(ContactMessageTypeGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<ContactMessageType> types = await db.ContactMessageType.Where(g => g.DeletedDate == null).ToListAsync(cancellationToken);

                return types;
            }
        }
    }
}
