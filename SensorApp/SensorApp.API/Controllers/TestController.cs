using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using SensorApp.API.Repository;
using SensorApp.API.Repository.Entity;

namespace SensorApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IRepositorySensor _productRepository;


        public TestController(ILogger<TestController> logger, IRepositorySensor productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpPut]
        [Route("/TestSaveToDatabase")]
        public String TestSaveToDatabase()
        {
            var rng = new Random();
            var entity = new SensorDB()
            {
                name = "Test1",
                type = "type1",
                value = 12,
                dateGenerate = DateTime.Now,
            };

            _productRepository.save(entity);
            return entity.ToJson();
        }
    }
}
