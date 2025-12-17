

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.DeleteCategory
{
    public class DeleteCategoryCommandHandler:
        IRequestHandler<DeleteCategoryCommand, ErrorOr<DeleteCategoryCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<DeleteCategoryCommandResponse>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
            if (category == null)
            {
                return Error.NotFound("Category.NotFound", $"Category with Id {request.Id} not found.");
            }
            var SubcategoryinCategory = await _unitOfWork.SubCategories.FindAsync(s=>s.CategoryId ==request.Id);
            if (SubcategoryinCategory.Any())
            {
                return Error.Validation("Category.HasSubCategories", $"Category with Id {request.Id} has associated subcategories and cannot be deleted.");
            }
            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.CompleteAsync();
            return new DeleteCategoryCommandResponse(category.Id);
        }
    }
}
