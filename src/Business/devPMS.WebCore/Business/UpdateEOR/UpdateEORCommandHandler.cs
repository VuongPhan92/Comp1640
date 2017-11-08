using devPMS.Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.Core.Repository;

namespace devPMS.WebCore.Business
{
    public class UpdateEORCommandHandler : ICommandHandler<UpdateEORCommand>
    {
        private readonly IDbContextFactory _factory;

        public UpdateEORCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(UpdateEORCommand command)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {                     
                var result = uow.Repository<Project_Log>().GetById(p=>p.ID==command.ID);
                result.EOR  = command.EOR;
                uow.Repository<Project_Log>().Update(result);
                uow.SubmitChanges();
            }
        }
    }
}