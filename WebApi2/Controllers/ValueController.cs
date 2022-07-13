using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System;

namespace WebApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValueController : ControllerBase
    {
        private readonly ILogger<ValueController> _logger;

        public ValueController(ILogger<ValueController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Output2 Post([FromBody] Input2 input)
        {
            // Simulation of business logic with a random value
            var rng = new Random();
            return new Output2 { Amount = rng.Next(1, 10) };
        }
    }
}
