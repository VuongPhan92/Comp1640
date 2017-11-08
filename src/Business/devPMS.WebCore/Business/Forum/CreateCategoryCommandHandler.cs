using University;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.PMS.Common;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace devPMS.WebCore.Business
{
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand>
    {
        private readonly IDbContextFactory _factory;

        public CreateCategoryCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(CreateCategoryCommand command)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                try
                {
                    var cat = new Category();
                    cat.CategoryName = command.Obj.CategoryName;
                    cat.UrlSeo = MixFunctions.ConvertURLSEO(command.Obj.CategoryName);
                    cat.CategoryDescription = command.Obj.CategoryDescription;
                    cat.Checked = command.Obj.Checked;                    
                    //
                    uow.Repository<Category>().Add(cat);
                    uow.SubmitChanges();
                    //
                    command.Obj.CategoryId = cat.CategoryId;
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