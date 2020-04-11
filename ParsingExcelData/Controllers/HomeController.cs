using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ParsingExcelData.Models;

namespace ParsingExcelData.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        [HttpPost]
        public IEnumerable Post([FromBody] string values)        
        {
            
            return null;
        }

        [HttpGet]
        public IEnumerable Get()
        {
            return null;
        }
    }
}