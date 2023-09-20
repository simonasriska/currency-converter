using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly CurrencyService _currencyService;

    public CurrencyController(CurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestRates()
    {
        try
        {
            var rates = await _currencyService.FetchExchangeRatesAsync();
            return Ok(rates);
        }
        catch (Exception ex)
        {
            // Logging the exception can be added here.
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}