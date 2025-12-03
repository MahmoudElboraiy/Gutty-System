

using Application.Interfaces.UnitOfWorkInterfaces;
using ErrorOr;
using MediatR;

namespace Application.Meals.Command.DeleteMeal;

public class DeleteMealCommandHandler : IRequestHandler<DeleteMealCommand, ErrorOr<ResultMessage>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteMealCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<ResultMessage>> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
    {
        var meal = await _unitOfWork.Meals.GetByIdAsync(request.Id);
        if (meal == null)
        {
            return Error.NotFound("Meal.NotFound", "Meal not found");
        }
        _unitOfWork.Meals.Remove(meal);
        await _unitOfWork.CompleteAsync();
        return new ResultMessage
        {
            Message = "Meal deleted successfully",
            Success = true
        };
    }
}
