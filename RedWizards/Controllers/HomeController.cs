using Microsoft.AspNetCore.Mvc;
using RedWizards.Models;
using System.Diagnostics;
using MySql.Data;
using RedWizards.Database;

namespace RedWizards.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public List<PageContent> GetAllProducts()
        {
            // alle info ophalen uit de database
            var rows = DatabaseConnector.GetRows("select * from product");

            // lijst maken om alles in te stoppen
            List<PageContent> paginas = new List<PageContent>();

            foreach (var row in rows)
            {
                // Voor elke rij een andere pagina
                PageContent p = new PageContent();
                p.Titel = row["titel"].ToString();
                // GA HIER VERDER AAN DATABASE
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}