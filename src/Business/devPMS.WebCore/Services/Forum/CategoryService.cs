using devPMS.WebCore.Business;
using Infrastructure.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Services
{
    public class CategoryService : IService
    {
        private readonly ICommandHandler<CreateCategoryCommand> _createCategory;
        private readonly ICommandHandler<UpdateCategoryCommand> _updateCategory;
        private readonly ICommandHandler<DeleteCategoryCommand> _deleteCategory;

        public CategoryService(ICommandHandler<CreateCategoryCommand> createCat,
            ICommandHandler<UpdateCategoryCommand> updateCat,
            ICommandHandler<DeleteCategoryCommand> deleteCat)
        {
            _createCategory = createCat;
            _updateCategory = updateCat;
            _deleteCategory = deleteCat;
        }

        public void CreateCat(CreateCategoryCommand command)
        {
            _createCategory.Handle(command);
        }

        public void UpdateCat(UpdateCategoryCommand command)
        {
            _updateCategory.Handle(command);
        }

        public void DeleteCat(DeleteCategoryCommand command)
        {
            _deleteCategory.Handle(command);
        }
    }
}
