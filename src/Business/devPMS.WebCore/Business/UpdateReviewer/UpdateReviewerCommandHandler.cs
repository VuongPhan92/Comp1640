using devPMS.Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.Core.Repository;

namespace devPMS.WebCore.Business
{
    public class UpdateReviewerCommandHandler : ICommandHandler<UpdateReviewerCommand>
    {
        private readonly IDbContextFactory _factory;

        public UpdateReviewerCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(UpdateReviewerCommand command)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {                     
                var result = uow.Repository<Project_Log>().GetById(p=>p.ID==command.ID);
                result.ReviewedBy  = command.Reviewer;
                uow.Repository<Project_Log>().Update(result);
                uow.SubmitChanges();
            }
        }
    }
}