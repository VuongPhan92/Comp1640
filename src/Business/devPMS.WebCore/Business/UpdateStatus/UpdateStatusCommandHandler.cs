using devPMS.Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.Core.Repository;

namespace devPMS.WebCore.Business
{
    public class UpdateStatusCommandHandler : ICommandHandler<UpdateStatusCommand>
    {
        private readonly IDbContextFactory _factory;

        public UpdateStatusCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(UpdateStatusCommand command)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {                     
                var result = uow.Repository<Project_Log>().GetById(p=>p.ID==command.ID);
                result.Status  = command.Status;
                uow.Repository<Project_Log>().Update(result);
                uow.SubmitChanges();
            }
        }
    }
}