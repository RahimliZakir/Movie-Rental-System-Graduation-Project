using MediatR;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.PersonalSideModule
{
    public class PersonalSideEditCommand : PersonalSideFormModel, IRequest<int>
    {
        public class PersonalSideEditCommandHandler : IRequestHandler<PersonalSideEditCommand, int>
        {


            async public Task<int> Handle(PersonalSideEditCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
