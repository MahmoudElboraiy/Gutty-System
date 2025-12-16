

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.UpdateSubCategory;

public class UpdateSubCategoryCommandHandler :
    IRequestHandler<UpdateSubCategoryCommand, ErrorOr<UpdateSubCategoryCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileService;
    public UpdateSubCategoryCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
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
        if(request.Image ==null || request.Image.Length==0)
        {
            return Error.Validation("SubCategory.ImageMissing", "Image file is required for the subcategory.");
        }
        var imageUrl = await _fileService.SaveImageAsync(request.Image);
        subCategory.Name = request.SubCategoryName;
        subCategory.ImageUrl = imageUrl;
        subCategory.CategoryId = request.CategoryId;
        await _unitOfWork.CompleteAsync();
        return new UpdateSubCategoryCommandResponse(subCategory.Id);
    }
}
