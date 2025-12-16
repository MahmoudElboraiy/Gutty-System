
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ErrorOr<CreateCategoryCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        return new CreateCategoryCommandResponse(category.Id);
    }
}
