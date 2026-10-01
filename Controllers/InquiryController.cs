using Calligraphy.Application.DTOs.Inquiry;
using Calligraphy.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InquiryController : ControllerBase
{
    private readonly IInquiryService _inquiryService;

    public InquiryController(IInquiryService inquiryService)
    {
        _inquiryService = inquiryService;
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateInquiryDto dto)
    {
        await _inquiryService.CreateInquiryAsync(dto);

        return Ok(new
        {
            message = "Inquiry created successfully."
        });
    }
}