using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Domain.Services.Identifier;

namespace TaxCalculator.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : Controller
{
    private readonly IIdentifierService _identifierService;

    public CurrencyController(IIdentifierService identifierService)
    {
        _identifierService = identifierService;
    }

    [HttpGet]
    public ActionResult<CurrencyIdentifier<Guid>> GetCurrencies()
    {
        return Ok(_identifierService.Currencies);
    }
}