using devPMS.Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.Core.Repository;

namespace devPMS.WebCore.Business
{
    public class UpdatePriorityIdCommandHandler : ICommandHandler<UpdatePriorityIdCommand>
    {
        private readonly IDbContextFactory _factory;

        public UpdatePriorityIdCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(UpdatePriorityIdCommand command)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {                     
                var result = uow.Repository<Project_Log>().GetById(p=>p.ID==command.ID);
                result.PriorityId  = command.PriorityId;
                uow.Repository<Project_Log>().Update(result);
                uow.SubmitChanges();
            }
        }
    }
}