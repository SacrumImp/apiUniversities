using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhru_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiUniversitiesController : ControllerBase
    {

        private readonly ILogger<ApiUniversitiesController> _logger;

        public ApiUniversitiesController(ILogger<ApiUniversitiesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public String PostUniversity(String identifier)
        {
            return identifier;
        }
    }
}
