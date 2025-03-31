using Application.Abstract;
using Business.Abstract;
using Domain.Concrete.Dtos;
using Domain.Results;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOrderManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService, IOrderDetailService orderDetailService) : ControllerBase
{
    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder(OrderInfoDto request)
    {
        Domain.Results.IResult result = await orderService.CreateOrder(request);

        return result.Success ? Ok(result) : BadRequest(result.Message);
    }

    [HttpGet("get-all-order-by-userid")]
    public async Task<IActionResult> GetAllOrderByUserId(int userId)
    {
        var result = await orderService.GetAllOrdersByUserId(userId);

        return result.Success ? Ok(result) : BadRequest(result.Message);
    }

    [HttpGet("get-order-details-by-orderid")]
    public async Task<IActionResult> GetOrderDetailsByOrderId(int orderId)
    {
        var result = await orderDetailService.GetOrderDetailsByOrderId(orderId);

        return result.Success ? Ok(result) : BadRequest(result.Message);
    }

    [HttpDelete("delete-order")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        var result = await orderService.DeleteOrder(orderId);
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }
}
