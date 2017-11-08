using devPMS.Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.Core.Repository;

namespace devPMS.WebCore.Business
{
    public class UpdateScopeTypeCommandHandler : ICommandHandler<UpdateScopeTypeCommand>
    {
        private readonly IDbContextFactory _factory;

        public UpdateScopeTypeCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(UpdateScopeTypeCommand command)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {                     
                var result = uow.Repository<Project_Log>().GetById(p=>p.ID==command.ID);
                result.ScopeTypeId  = command.ScopeTypeId;
                uow.Repository<Project_Log>().Update(result);
                uow.SubmitChanges();
            }
        }
    }
}