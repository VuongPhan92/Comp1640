using devPMS.Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.Core.Repository;

namespace devPMS.WebCore.Business
{
    public class UpdateDescriptionOfServiceCommandHandler : ICommandHandler<UpdateDescriptionOfServiceCommand >
    {
        private readonly IDbContextFactory _factory;

        public UpdateDescriptionOfServiceCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(UpdateDescriptionOfServiceCommand command)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {                     
                var result = uow.Repository<Project_Log>().GetById(p=>p.ID==command.ID);
                result.DescriptionOfService  = command.DescriptionOfService;
                uow.Repository<Project_Log>().Update(result);
                uow.SubmitChanges();
            }
        }
    }
}