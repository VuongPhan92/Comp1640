using University;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace devPMS.WebCore.Business
{
    public class AddThreadCommentCommandHandler : ICommandHandler<AddThreadCommentCommand>
    {
        private readonly IDbContextFactory _factory;

        public AddThreadCommentCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public void Handle(AddThreadCommentCommand command)
        {
            using (var uow = _factory.Create(DBContextName.UniversityEntities))
            {
                var comment = new Comment();
                comment.ThreadId = command.ThreadId;
                comment.UserId = command.UserId;
                comment.Body = command.Body;
                comment.CreatedDT = DateTime.Now;
                uow.Repository<Comment>().Add(comment);
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