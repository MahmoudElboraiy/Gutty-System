
using MediatR;

namespace Application.Orders.Commands.SelectDays;

public record SelectDaysCommand(uint SelectedDays): IRequest<SelectDaysCommandResponse>;
public record SelectDaysCommandResponse
    (bool success,string message ,uint SelectedDays,uint BDNumberOfMeals, uint  LNumberOfMeals);