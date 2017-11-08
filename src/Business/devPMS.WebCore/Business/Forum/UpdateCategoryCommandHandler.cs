using University;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.PMS.Common;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace devPMS.WebCore.Business
{
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand>
    {
        private readonly IDbContextFactory _factory;

        public UpdateCategoryCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(UpdateCategoryCommand command)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                try
                {
                    var cat = uow.Repository<Category>().Get(command.Obj.CategoryId);
                    if (cat != null)
                    {
                        cat.CategoryName = command.Obj.CategoryName;
                        cat.UrlSeo = MixFunctions.ConvertURLSEO(command.Obj.CategoryName);
                        cat.CategoryDescription = command.Obj.CategoryDescription;
                        cat.Checked = command.Obj.Checked;                    
                        //
                        uow.Repository<Category>().Update(cat);
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