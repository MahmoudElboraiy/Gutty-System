using System.Runtime.InteropServices.JavaScript;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("PerUser")]
public class EnumsController : ControllerBase
{
    
    [HttpGet("itemMenuType")]
    public IActionResult GetItemMenuType()
    {
        //return Enum.GetNames(typeof(ItemMenuType));
        var result = Enum.GetValues(typeof(ItemMenuType))
           .Cast<ItemMenuType>()
           .Select(e => new {
               Name = e.ToString(),
               Value = (int)e
           });

        return Ok(result);
    }
    
    [HttpGet("itemType")]
    public IActionResult GetItemType()
    {
         //return Enum.GetNames(typeof(ItemType));
        var result = Enum.GetValues(typeof(ItemType))
           .Cast<ItemType>()
           .Select(e => new {
               Name = e.ToString(),
               Value = (int)e
           });

        return Ok(result);
    }
    
    [HttpGet("mealType")]
    public IActionResult GetMealType()
    {
        // return Enum.GetNames(typeof(MealType));
        var result = Enum.GetValues(typeof(MealType))
           .Cast<MealType>()
           .Select(e => new {
               Name = e.ToString(),
               Value = (int)e
           });

        return Ok(result);
    }
    
    [HttpGet("discount-types")]
    public IActionResult GetDiscountTypes()
    {
        var result = Enum.GetValues(typeof(DiscountType))
            .Cast<DiscountType>()
            .Select(e => new {
                Name = e.ToString(),
                Value = (int)e
            });

        return Ok(result);
    }
    [HttpGet("RoleTypes")]
    public IActionResult GetRoleTypes()
    {
        var result = Enum.GetValues(typeof(Roles))
            .Cast<Roles>()
            .Select(e => new
            {
                Name = e.ToString(),
                Value = (int)e
            });
        return Ok(result);
    }
    [HttpGet("SaleType")]
    public IActionResult GetSaleType()
    {
        var result = Enum.GetValues(typeof(SaleType))
            .Cast<SaleType>()
            .Select(e => new
            {
                Name = e.ToString(),
                Value = (int)e
            });
        return Ok(result);
    }
    [HttpGet("UnitType")]
    public IActionResult GetUnitType()
    {
        var result = Enum.GetValues(typeof(UnitType))
            .Cast<UnitType>()
            .Select(e => new
            {
                Name = e.ToString(),
                Value = (int)e
            });
        return Ok(result);
    }
}
public static class EnumExtensions
{
    public static IEnumerable<object> ToList<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e => new
            {
                Value = Convert.ToInt32(e),
                Name = e.ToString()
            });
    }
}