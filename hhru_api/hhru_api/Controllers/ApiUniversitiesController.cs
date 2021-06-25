using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using System.Collections.Generic;

using System.Threading.Tasks;

using hhru_api.Models;
using hhru_api.Entities;

namespace hhru_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiUniversitiesController : ControllerBase
    {

        private readonly ILogger<ApiUniversitiesController> _logger;
        private SqlDbContext _context;

        private static ProductInfoHeaderValue productValue = new ProductInfoHeaderValue("UniversityAPI", "1.0");

        private static readonly HttpClient client = new HttpClient();

        private static string baseURL = "https://api.hh.ru/";

        private University university;
        private List<Faculty> faculties;


        public ApiUniversitiesController(ILogger<ApiUniversitiesController> logger, SqlDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task PostUniversity(string identifier)
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
            
            if (university == null || faculties == null)
                Response.StatusCode = 400;
            else 
                await saveUniversityData();

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

        private async Task saveUniversityData()
        {
            AreaEntity areaEntity = new AreaEntity(Int32.Parse(university.area.id), university.area.name);
            if (_context.Area.Find(areaEntity.id) == null)
                _context.Area.Add(areaEntity);

            UniversityEntity universityEntity = new UniversityEntity(Int32.Parse(university.id), university.acronym, university.text, university.synonyms, Int32.Parse(university.area.id));
            if (_context.Universities.Find(universityEntity.id) == null)
                _context.Universities.Add(universityEntity);

            FacultyEntity facultyEntity;
            foreach (Faculty faculty in faculties)
            {
                facultyEntity = new FacultyEntity(Int32.Parse(faculty.id), Int32.Parse(university.id), faculty.name);
                if (_context.Faculties.Find(facultyEntity.id) == null)
                    _context.Faculties.Add(facultyEntity);
            }

            await _context.SaveChangesAsync();
        }
    }
}
