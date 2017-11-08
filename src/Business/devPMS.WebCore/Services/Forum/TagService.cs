using devPMS.WebCore.Business;
using Infrastructure.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Services
{
    public class TagService : IService
    {
        private readonly ICommandHandler<CreateTagCommand> _createTag;
        private readonly ICommandHandler<UpdateTagCommand> _updateTag;
        private readonly ICommandHandler<DeleteTagCommand> _deleteTag;

        public TagService(ICommandHandler<CreateTagCommand> createTag,
            ICommandHandler<UpdateTagCommand> updateTag,
            ICommandHandler<DeleteTagCommand> deleteTag)
        {
            _createTag = createTag;
            _updateTag = updateTag;
            _deleteTag = deleteTag;
        }

        public void CreateTag(CreateTagCommand command)
        {
            _createTag.Handle(command);
        }

        public void UpdateTag(UpdateTagCommand command)
        {
            _updateTag.Handle(command);
        }

        public void DeleteTag(DeleteTagCommand command)
        {
            _deleteTag.Handle(command);
        }
    }
}
