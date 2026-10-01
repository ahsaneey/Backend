using System.Security.Claims;
using Calligraphy.Application.DTOs.Payment;
using Calligraphy.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("create/{orderId}")]
    public async Task<ActionResult<CreatePaymentResponseDto>> CreatePayment(
        int orderId)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _paymentService.CreatePaymentAsync(
            orderId,
            userId);

        return Ok(result);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyPayment(
        VerifyPaymentDto dto)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var verified =
            await _paymentService.VerifyPaymentAsync(
                dto,
                userId);

        if (!verified)
        {
            return BadRequest("Payment verification failed.");
        }

        return Ok(new
        {
            message = "Payment verified successfully."
        });
    }
}