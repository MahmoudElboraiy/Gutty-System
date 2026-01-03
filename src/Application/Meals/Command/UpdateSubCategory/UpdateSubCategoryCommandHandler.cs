

using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.UpdateSubCategory;

public class UpdateSubCategoryCommandHandler :
    IRequestHandler<UpdateSubCategoryCommand, ErrorOr<UpdateSubCategoryCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileService;
    private readonly ICacheService _cacheService;
    public UpdateSubCategoryCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileService, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<UpdateSubCategoryCommandResponse>> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var subCategory = await _unitOfWork.SubCategories.GetByIdAsync(request.SubCategoryId);
        if (subCategory == null)
        {
            return Error.NotFound("SubCategory.NotFound", $"SubCategory with Id {request.SubCategoryId} not found.");
        }
        var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            return Error.NotFound("Category.NotFound", $"Category with Id {request.CategoryId} not found.");
        }
        string imageUrl = subCategory.ImageUrl;
        if (request.Image != null && request.Image.Length > 0)
        {
             imageUrl = await _fileService.SaveImageAsync(request.Image);
        }
        subCategory.Name = request.SubCategoryName;
        subCategory.ImageUrl = imageUrl;
        subCategory.CategoryId = request.CategoryId;
        _cacheService.IncrementVersion(CacheKeys.SubCategoriesVersion);
        _cacheService.IncrementVersion(CacheKeys.MealsVersion);
        await _unitOfWork.CompleteAsync();
        return new UpdateSubCategoryCommandResponse(subCategory.Id);
    }
}
