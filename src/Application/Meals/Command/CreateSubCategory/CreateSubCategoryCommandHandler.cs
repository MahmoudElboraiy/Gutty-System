

using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.CreateSubCategory;

public class CreateSubCategoryCommandHandler :
    IRequestHandler<CreateSubCategoryCommand, ErrorOr<CreateSubCategoryCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileService;
    public CreateSubCategoryCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }
    public async Task<ErrorOr<CreateSubCategoryCommandResponse>> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            return Error.NotFound("Category.NotFound", $"Category with Id {request.CategoryId} not found.");
        }
        if(request.Image == null || request.Image.Length == 0)
        {
            return Error.Validation("SubCategory.ImageMissing", "Image file is required for the subcategory.");
        }
        var imageUrl = await _fileService.SaveImageAsync(request.Image);
        var subCategory = new Subcategory
        {
            Name = request.SubCategoryName,
            ImageUrl = imageUrl,
            CategoryId = request.CategoryId
        };
        await _unitOfWork.SubCategories.AddAsync(subCategory);
        await _unitOfWork.CompleteAsync();
        return new CreateSubCategoryCommandResponse(subCategory.Id);
    }

}
