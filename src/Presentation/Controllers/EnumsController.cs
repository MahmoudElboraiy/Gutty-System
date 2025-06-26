using System.Runtime.InteropServices.JavaScript;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnumsController
{
    
    [HttpGet("itemMenuType")]
    public IEnumerable<string> GetItemMenuType()
    {
        return Enum.GetNames(typeof(ItemMenuType));
    }
    
    [HttpGet("itemType")]
    public IEnumerable<string> GetItemType()
    {
        return Enum.GetNames(typeof(ItemType));
    }
    
    [HttpGet("mealType")]
    public IEnumerable<string> GetMealType()
    {
        return Enum.GetNames(typeof(MealType));
    }
    
    [HttpGet("paymentMethod")]
    public IEnumerable<string> GetPaymentMethod()
    {
        return Enum.GetNames(typeof(PaymentMethod));
    }
    
    [HttpGet("paymentStatus")]
    public IEnumerable<string> GetPaymentStatus()
    {
        return Enum.GetNames(typeof(PaymentStatus));
    }
}