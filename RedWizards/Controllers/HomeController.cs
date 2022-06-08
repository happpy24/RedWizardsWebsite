using Microsoft.AspNetCore.Mvc;
using RedWizards.Database;
using RedWizards.Models;
using System.Diagnostics;

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
                PageContent p = RowToPageContent(row);

                pageContent.Add(p);
            }

            return pageContent;
        }      

        public IActionResult Index()
        {
            ViewData["navigation"] = GetAllPages();

            return View();
        }

        [Route("organisatie")]
        public IActionResult Organisatie()
        {
            ViewData["navigation"] = GetAllPages();

            return View();
        }

        [Route("winkelwagen")]
        public IActionResult Winkelwagen()
        {
            ViewData["navigation"] = GetAllPages();

            return View();
        }

        [Route("vacatures")]
        public IActionResult Vacatures()
        {
            ViewData["navigation"] = GetAllPages();

            return View();
        }

        [Route("contact")]
        public IActionResult Contact()
        { 
            ViewData["navigation"] = GetAllPages();

            return View();
        }

        [HttpPost]
        [Route("contact")]
        public IActionResult Contact(Person person)
        {
            if (ModelState.IsValid) { 
                DatabaseConnector.SavePerson(person);
            }

            return View(person);
        }

        [Route("pagina/{id}")]
        public IActionResult PageContent(int id)
        {
            var rows = DatabaseConnector.GetRows($"select * from pagecontent where id = {id}");

            // lijst maken om alles in te stoppen
            List<PageContent> pageContent = new List<PageContent>();

            foreach (var row in rows)
            {
                // Voor elke rij een andere pagina
                PageContent p = RowToPageContent(row);

                pageContent.Add(p);
            }

            return View(pageContent[0]);
        }

        private static PageContent RowToPageContent(Dictionary<string, object> row)
        {
            PageContent p = new PageContent();
            p.Titel = row["titel"].ToString();
            p.Tekst = row["tekst"].ToString();
            p.Image = row["image"].ToString();
            p.Button = row["button"].ToString();
            p.Date = row["date"].ToString();
            p.Id = Convert.ToInt32(row["id"]);
            return p;
        }

        [Route("voorstelling/{id}")]
        public IActionResult Voorstellingen(int id)
        {
            var rows = DatabaseConnector.GetRows($"select * from voorstellingen where id = {id}");

            // lijst maken om alles in te stoppen
            List<Voorstelling> pageContent = new List<Voorstelling>();

            foreach (var row in rows)
            {
                // Voor elke rij een andere pagina
                Voorstelling p = new Voorstelling();
                p.Categorie = row["categorie"].ToString();
                p.Naam = row["naam"].ToString();
                p.Datum = row["datum"].ToString();
                p.Beschrijving_Kort = row["beschrijving_kort"].ToString();
                p.Beschrijving_Lang = row["beschrijving_lang"].ToString();
                p.Image = row["image"].ToString();
                p.Minleeftijd = row["minleeftijd"].ToString();
                p.Prijs_1erang = row["prijs_1erang"].ToString();
                p.Prijs_2erang = row["prijs_2erang"].ToString();
                p.Prijs_3erang = row["prijs_3erang"].ToString();
                p.Id = Convert.ToInt32(row["id"]);

                pageContent.Add(p);
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}