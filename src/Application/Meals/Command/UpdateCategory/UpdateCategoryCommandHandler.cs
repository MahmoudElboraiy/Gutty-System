

using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.UpdateCategory;

public class UpdateCategoryCommandHandler :
    IRequestHandler<UpdateCategoryCommand, ErrorOr<UpdateCategoryCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<UpdateCategoryCommandResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
        if (category == null)
        {
            return Error.NotFound("Category.NotFound", $"Category with Id {request.Id} not found.");
        }
        category.Name = request.Name;
        category.MealType = request.MealType;
        _unitOfWork.Categories.Update(category);
        await _unitOfWork.CompleteAsync();
        _cacheService.IncrementVersion(CacheKeys.CategoriesVersion);
        return new UpdateCategoryCommandResponse(category.Id);
    }
}
