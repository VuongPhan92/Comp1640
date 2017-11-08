using University;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace devPMS.WebCore.Business
{
    public class UpdateThreadCommentCommandHandler : ICommandHandler<UpdateThreadCommentCommand>
    {
        private readonly IDbContextFactory _factory;

        public UpdateThreadCommentCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public void Handle(UpdateThreadCommentCommand command)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                var commentEntity = uow.Repository<Comment>().GetById(p => p.ThreadId == command.ThreadId && p.UserId == command.UserId && p.CommentId == command.CommentId);
                commentEntity.Body = command.Body;
                try
                {
                    uow.SubmitChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
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