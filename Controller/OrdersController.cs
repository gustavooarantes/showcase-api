using FoodNet.DTOs;
using FoodNet.Service;
using Microsoft.AspNetCore.Mvc;

namespace FoodNet.Controller;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> CreateOrder(CreateOrderDto dto)
    {
        try
        {
            var order = await _service.CreateOrderAsync(dto);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }
            // Simple error handling for now
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders()
    {
        var orders = await _service.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrder(int id)
    {
        var order = await _service.GetOrderByIdAsync(id);
        if (order == null) return NotFound();
        
        return Ok(order);
    }
}