
using Application.Cache;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ErrorOr<CreateCategoryCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork,ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }
    public async Task<ErrorOr<CreateCategoryCommandResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            MealType = request.MealType
        };
        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.CompleteAsync();
        _cacheService.IncrementVersion(CacheKeys.CategoriesVersion);
        return new CreateCategoryCommandResponse(category.Id);
    }
}
