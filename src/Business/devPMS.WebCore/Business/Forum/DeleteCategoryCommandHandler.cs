using University;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.PMS.Common;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace devPMS.WebCore.Business
{
    public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly IDbContextFactory _factory;

        public DeleteCategoryCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(DeleteCategoryCommand command)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                try
                {
                    uow.Repository<Category>().Remove(command.CatId);
                    uow.SubmitChanges();
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