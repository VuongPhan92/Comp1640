using devPMS.Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.Core.Repository;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace devPMS.WebCore.Business
{
    // Implementation for the "create project" use case.
    public class AddProjectCommentCommandHandler : ICommandHandler<AddProjectCommentCommand>
    {
        private readonly IDbContextFactory _factory;

        public AddProjectCommentCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public void Handle(AddProjectCommentCommand cm)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {
                var comment = new ProjectComment();
                comment.ProjectId = cm.ProjectId;
                comment.UserId = cm.UserId;
                comment.Body = cm.Body;
                //system field
                comment.CreatedDT = System.DateTime.Now;
                comment.ModifiedDT = System.DateTime.Now;

                uow.Repository<ProjectComment>().Add(comment);
                try
                {
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