using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParsingExcelData.Models;

namespace ParsingExcelData.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ResultsController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            try {
                NewFile newFile = new NewFile(file);
                //Results results = new Results();
                //results.Data = newFile.data;
                return Json(newFile.data);

            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}