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

        public List<PageContent> GetAllPages()
        {
            // alle info ophalen uit de database
            var rows = DatabaseConnector.GetRows("select * from `pagecontent`");

            // lijst maken om alles in te stoppen
            List<PageContent> pageContent = new List<PageContent>();

            foreach (var row in rows)
            {
                // Voor elke rij een andere pagina
                PageContent p = new PageContent();
                p.Titel = row["titel"].ToString();
                p.Tekst = row["tekst"].ToString();
                p.Image = row["image"].ToString();
                p.Button = row["button"].ToString();
                p.Date = row["date"].ToString();
                p.Id = Convert.ToInt32(row["id"]);

                pageContent.Add(p);
            }

            return pageContent;
        }

        public IActionResult Index()
        {
            ViewData["navigation"] = GetAllPages();

            return View();
        }


        public IActionResult Privacy()
        {
            ViewData["navigation"] = GetAllPages();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}