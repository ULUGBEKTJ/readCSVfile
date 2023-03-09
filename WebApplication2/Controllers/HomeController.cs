using CsvHelper;
using Eco.ViewModel.Runtime;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.IO;
using System.Threading.Tasks;
using WebApplication2.Classes;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //connect to the database and download all data to the list <t> 
            DB_Import data_Import = new DB_Import(@"Server=LEGION\SQLEXPRESS;Database=D_BASE_1;Trusted_Connection=True");
            List<ModelDB> data = await data_Import.Load_From_DB();
            //send everything to the page
            ViewBag.data = data;
            //dispose all resource
            data_Import.Quit();
            
            return View();
        }
         

        public IActionResult Privacy()
        {

            return View();
        }


        //method that downloads the file 
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            //Get the file from the form
            using var memoryStream = new MemoryStream(new byte[file.Length]);
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            List<ModelDB> data = new();
            using (var reader = new StreamReader(memoryStream))
            {
                TextFieldParser streamData = new TextFieldParser(reader);
                CSV_Impoert csvreader = new CSV_Impoert();

                //read everything from the csv file and write it into the database table
                data = await csvreader.Load_from_CSV(streamData);

                DB_Import import = new DB_Import();
                await import.Add_to_DB(data);
                await import.Quit();

            }

            return RedirectToAction("index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
