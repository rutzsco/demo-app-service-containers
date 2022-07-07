using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Controllers
{
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IConfiguration _configuration;

        public StatusController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpGet("status")]
        public IActionResult Get()
        {
            var version = _configuration["APPLICATION_VERSION"];
            var vm = new { Version = version };
            return new OkObjectResult(vm);
        }

        [HttpGet("status/full")]
        public IEnumerable<KeyValuePair<string, string>> GetFull()
        {
            var values = _configuration.AsEnumerable().Select(c => new KeyValuePair<string,string>(c.Key,c.Value));
            return values;

            //var variables = new List<KeyValuePair<string, string>>();
            //foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            //    variables.Add(new KeyValuePair<string, string>(de.Key.ToString(), de.Value.ToString()));
            //return variables;
        }

        [HttpGet("status/file")]
        public IActionResult GetFile()
        {
            var values = _configuration.AsEnumerable().Select(c => new KeyValuePair<string, string>(c.Key, c.Value));
            using (StreamWriter outputFile = new StreamWriter(Path.Combine("/files", $"{Guid.NewGuid()}.txt")))
            {
                outputFile.WriteLine("TEST");
            }
            return new OkObjectResult("OK");
        }
    }
}
