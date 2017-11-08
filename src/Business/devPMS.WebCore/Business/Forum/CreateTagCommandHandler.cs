using University;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.PMS.Common;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace devPMS.WebCore.Business
{
    public class CreateTagCommandHandler : ICommandHandler<CreateTagCommand>
    {
        private readonly IDbContextFactory _factory;

        public CreateTagCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(CreateTagCommand command)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                try
                {
                    var tag = new Tag();
                    tag.TagName = command.Obj.TagName;
                    tag.UrlSeo = MixFunctions.ConvertURLSEO(command.Obj.TagName);
                    tag.Checked = command.Obj.Checked;                    
                    //
                    uow.Repository<Tag>().Add(tag);
                    uow.SubmitChanges();
                    //
                    command.Obj.TagId = tag.TagId;
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