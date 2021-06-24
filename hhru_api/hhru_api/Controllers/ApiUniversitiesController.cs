using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace hhru_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiUniversitiesController : ControllerBase
    {

        private readonly ILogger<ApiUniversitiesController> _logger;

        private static ProductInfoHeaderValue productValue = new ProductInfoHeaderValue("UniversityAPI", "1.0");

        private static readonly HttpClient client = new HttpClient();

        private static string baseURL = "https://api.hh.ru/";

        private University university;
        private List<Faculty> faculties;


        public ApiUniversitiesController(ILogger<ApiUniversitiesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void PostUniversity(string identifier)
        {
            if (identifier == null)
            {
                Response.StatusCode = 400;
                return;
            }

            if (!getUniversityData(identifier))
                Response.StatusCode = 400;
            else
                Response.StatusCode = 200;

            return;
        }

        private bool getUniversityData(string ID)
        {
            client.DefaultRequestHeaders.UserAgent.Add(productValue);
            Uri uri;
            string responseBody;

            try
            {
                uri = new Uri(baseURL + "educational_institutions/?id=" + ID);
                var response = client.GetAsync(uri).Result;
                responseBody = response.Content.ReadAsStringAsync().Result;

                Universities universities = JsonConvert.DeserializeObject<Universities>(responseBody);
                university = universities.getFirst();
            }
            catch
            {
                return false;
            }

            try
            {
                uri = new Uri(baseURL + "educational_institutions/" + ID + "/faculties");
                var response = client.GetAsync(uri).Result;
                responseBody = response.Content.ReadAsStringAsync().Result;

                faculties = JsonConvert.DeserializeObject<List<Faculty>>(responseBody);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
