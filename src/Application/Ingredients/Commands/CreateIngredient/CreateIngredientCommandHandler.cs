using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Enums;
using Domain.Models.Entities;
using ErrorOr;
using MediatR;

namespace Application.Ingredients.Commands.CreateIngredient;

public class CreateIngredientCommandHandler
    : IRequestHandler<CreateIngredientCommand, ErrorOr<CreateIngredientCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IIngredientLogRepository _ingredientLogRepository;

    public CreateIngredientCommandHandler(
        IIngredientRepository ingredientRepository,
        IUnitOfWork unitOfWork,
        IIngredientLogRepository ingredientLogRepository
    )
    {
        _ingredientRepository = ingredientRepository;
        _unitOfWork = unitOfWork;
        _ingredientLogRepository = ingredientLogRepository;
    }

    public async Task<ErrorOr<CreateIngredientCommandResponse>> Handle(
        CreateIngredientCommand request,
        CancellationToken cancellationToken
    )
    {
        var ingredient = new Ingredient() { Name = request.Name, Stock = request.Stock };

        await _ingredientRepository.AddAsync(ingredient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (request.Stock != 0)
        {
            await _ingredientLogRepository.AddAsync(
                new IngredientLog()
                {
                    IngredientId = ingredient.Id,
                    Date = DateTime.Now,
                    Quantity = Convert.ToInt32(request.Stock),
                    Status = IngredientStatus.Deposit,
                }
            );
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateIngredientCommandResponse(
            ingredient.Id,
            ingredient.Name,
            ingredient.Stock
        );
    }
}
