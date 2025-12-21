

using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.DeleteSubCategory;

public class DeleteSubCategoryCommandHandler:
    IRequestHandler<DeleteSubCategoryCommand, ErrorOr<DeleteSubCategoryCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    public DeleteSubCategoryCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<DeleteSubCategoryCommandResponse>> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var subCategory = await _unitOfWork.SubCategories.GetByIdAsync(request.SubCategoryId);
        if (subCategory == null)
        {
            return Error.NotFound("SubCategory.NotFound", $"SubCategory with Id {request.SubCategoryId} not found.");
        }
        var mealsInSubCategory = await _unitOfWork.Meals.FindAsync(m => m.SubcategoryId == request.SubCategoryId);
        if (mealsInSubCategory.Any())
        {
            return Error.Validation("SubCategory.HasMeals", $"SubCategory with Id {request.SubCategoryId} has associated meals and cannot be deleted.");
        }
        _cacheService.IncrementVersion(CacheKeys.SubCategoriesVersion);
        _cacheService.IncrementVersion(CacheKeys.MealsVersion);
        _unitOfWork.SubCategories.Remove(subCategory);
        await _unitOfWork.CompleteAsync();
        return new DeleteSubCategoryCommandResponse(subCategory.Id);
    }
}
