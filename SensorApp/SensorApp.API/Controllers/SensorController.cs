using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SensorApp.API.Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using SensorApp.API.Querys.GetAllSensors;
using System.Linq;
using SensorApp.API.Querys.GetAverageandLastSensors;

namespace SensorApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly IMediator _mediator;

        public SensorController(ILogger<TestController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/GetAll")]
        public async Task<ActionResult<List<SensorDB>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllSensorQuery());

            return await Task.FromResult(result.ToList());
        }

        [HttpGet]
        [Produces(contentType: "text/csv","text/json")]
        [Route("/Last")]
        public async Task<ActionResult<List<SensorLastAndAvergeDTO>>> GetLastAndAvgValue()
        {
            var result = await _mediator.Send(new GetAverageandLastSensorsQuery());

            return await Task.FromResult(result.ToList());
        }
    }
}
