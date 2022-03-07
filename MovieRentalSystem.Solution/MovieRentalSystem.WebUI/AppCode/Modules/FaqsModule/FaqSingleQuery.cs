using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.FaqsModule
{
    public class FaqSingleQuery : IRequest<FaqViewModel>
    {
        public int? Id { get; set; }

        public class FaqSingleQueryHandler : IRequestHandler<FaqSingleQuery, FaqViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public FaqSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<FaqViewModel> Handle(FaqSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                Faq faq = await db.Faqs.FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                FaqViewModel viewModel = mapper.Map<FaqViewModel>(faq);

                return viewModel;
            }
        }
    }
}
