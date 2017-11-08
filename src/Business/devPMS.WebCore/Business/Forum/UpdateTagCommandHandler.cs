using University;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.PMS.Common;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace devPMS.WebCore.Business
{
    public class UpdateTagCommandHandler : ICommandHandler<UpdateTagCommand>
    {
        private readonly IDbContextFactory _factory;

        public UpdateTagCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(UpdateTagCommand command)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                try
                {
                    var tag = uow.Repository<Tag>().Get(command.Obj.TagId);
                    if (tag != null)
                    {
                        tag.TagName = command.Obj.TagName;
                        tag.UrlSeo = MixFunctions.ConvertURLSEO(command.Obj.TagName);
                        tag.Checked = command.Obj.Checked;                    
                        //
                        uow.Repository<Tag>().Update(tag);
                        uow.SubmitChanges();
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
        }
    }
}