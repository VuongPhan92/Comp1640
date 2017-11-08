using devForum.Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;

namespace devPMS.WebCore.Business
{
    public class CreateThreadCommandHandler : ICommandHandler<CreateThreadCommand>
    {
        private readonly IDbContextFactory _factory;

        public CreateThreadCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }
        public void Handle(CreateThreadCommand command)
        {
            var thread = new Thread();
            thread.UserId = command.UserId ;
            thread.CategoryId  = command.CategoryId  ;
            thread.Title = command.Title ;
            thread.ShortDescription = command.ShortDescription;
            thread.Body = command.Body;
            thread.Meta = command.Meta;
            thread.UrlSeo = command.UrlSeo;
            thread.IsPublished = command.IsPublished;
            thread.View = command.View;
            thread.Image = command.Image;
            thread.CreatedDT = command.CreatedDT;
            thread.ModifiedDT = command.ModifiedDT;
            thread.DeletedDT = command.DeletedDT;
        }
        }
}