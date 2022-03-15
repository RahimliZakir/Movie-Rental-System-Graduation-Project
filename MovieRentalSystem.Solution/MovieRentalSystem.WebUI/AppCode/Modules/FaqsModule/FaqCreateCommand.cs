using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.FaqsModule
{
    public class FaqCreateCommand : FaqViewModel, IRequest<CommandJsonResponse>
    {
        public class FaqCreateCommandHandler : IRequestHandler<FaqCreateCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public FaqCreateCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
            }

            public async Task<CommandJsonResponse> Handle(FaqCreateCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (!ctx.IsValid())
                {
                    response.Error = true;
                    response.Message = "Məlumat düzgün göndərilməyib!";
                }

                if (ctx.IsValid())
                {
                    Faq faq = mapper.Map<Faq>(request);
                    faq.CreatedByUserId = ctx.GetUserId();

                    await db.Faqs.AddAsync(faq, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Məlumat uğurla əlavə olundu!";
                }

                return response;
            }
        }
    }
}
