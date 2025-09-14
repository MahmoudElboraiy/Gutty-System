using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Meals.Commands.Delete;
public record DeleteMealCommandResponse(
    Guid Id,
    bool IsDeleted
);