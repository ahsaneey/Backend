using Calligraphy.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Api.Controllers;

[ApiController]
[Route("api/admin/orders")]
[Authorize(Roles = "Admin")]
public class AdminOrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public AdminOrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders(
        [FromQuery] string ? status = null)
    {
        var orders = await _orderService.GetAllOrdersAsync(status);

        var result = orders.Select(order => new
        {
            order.Id,
            order.UserId,
            order.TotalAmount,
            order.Status,
            order.ShippingAddress,
            order.CreatedAt,

            OrderItems = order.OrderItems.Select(item => new
            {
                item.Id,
                item.ProductId,
                item.Quantity,
                item.Price
            })
        });

        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);

        if (order == null)
            return NotFound("Order not found.");

        return Ok(new
        {
            order.Id,
            order.UserId,
            order.TotalAmount,
            order.Status,
            order.ShippingAddress,
            order.CreatedAt,

            OrderItems = order.OrderItems.Select(item => new
            {
                item.Id,
                item.ProductId,
                item.Quantity,
                item.Price
            })
        });
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(
        int id,
        [FromQuery] string status)
    {
        var updated =
            await _orderService.UpdateOrderStatusAsync(
                id,
                status);

        if (!updated)
            return NotFound("Order not found.");

        return Ok(new
        {
            message = "Order status updated successfully."
        });
    }
}