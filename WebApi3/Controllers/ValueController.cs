using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System;

namespace WebApi3.Controllers
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
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public Output3 Post([FromBody] Input3 input)        
        {
            // Simulation of business logic with a random value
            var rng = new Random();
            return new Output3 { Quote = rng.Next(1, 10) };
            
        }
    }
}
