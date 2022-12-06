using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SensorApp.API.Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using SensorApp.API.Querys.GetAllSensors;
using System.Linq;
using SensorApp.API.Querys.GetAllSensors.GetFilterSensor;
using SensorApp.API.Querys.GetAverageandLastSensors;
using SensorApp.API.Querys.GetDatesAndValueSensors;

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
        public async Task<ActionResult<List<SensorWithSplitDate>>> GetAll()
        {
            var result = await _mediator.Send(new GetFilterSensorQuery()
            {
                Name = null,
                Value_from = null,
                Value_to = null,
                Type = null,
                SortBy = null,
                TypSort = null
            });

            return await Task.FromResult(result.ToList());
        }

        [HttpPost]
        [Route("/GetAll2")]
        public async Task<ActionResult<List<SensorWithSplitDate>>> GetAll2([FromBody] Message mess)
        {
            var result = await _mediator.Send(new GetFilterSensorQuery()
            {
                Name= mess.Name,
                Value_from = mess.Value_from,
                Value_to = mess.Value_to,
                Type = mess.Type,
                SortBy = mess.SortBy,
                TypSort = mess.TypSort,
                Date_from = mess.Date_from,
                Date_to = mess.Date_to
            });

            return await Task.FromResult(result.ToList());
        }

        [HttpPost]
        [Route("/Chart")]
        public async Task<ActionResult<SensorListDataAndValue>> GetChart([FromBody] RequestMessage mes2s)
        {
            var result = await _mediator.Send(new GetDatesAndValueSensorsQuery()
            {
                mess = mes2s
            });

            return await Task.FromResult(result);
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
