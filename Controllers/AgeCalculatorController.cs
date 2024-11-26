using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgeCalculatorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgeCalculatorController : ControllerBase
    {
        [HttpGet("calculate-age")]
        public IActionResult CalculateAge(DateTime birthDate)
        {
            if (birthDate > DateTime.Now)
            {
                return BadRequest("Birth date cannot be in the future.");
            }

           
            DateTime today = DateTime.Today;

            // Calculate years
            int years = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-years)) years--;

            // Calculate months
            int months = today.Month - birthDate.Month;
            if (months < 0)
            {
                months += 12;
                years--;
            }

            // Calculate days
            int days = today.Day - birthDate.Day;
            if (days < 0)
            {
                var prevMonth = today.AddMonths(-1);
                days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
                months--;
                if (months < 0)
                {
                    months += 12;
                    years--;
                }
            }

            return Ok(new { Years = years, Months = months, Days = days });
        }
    }
}

