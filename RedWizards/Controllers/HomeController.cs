using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IActionResult Index()
        {
            return View();
        }

        [Route("organisatie")]
        public IActionResult Organisatie()
        {
            return View();
        }

        [Route("winkelwagen")]
        public IActionResult Winkelwagen()
        {
            return View();
        }

        [Route("vacatures")]
        public IActionResult Vacatures()
        {
            return View();
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Route("contact")]
        public IActionResult Contact(Person person)
        {
            if (ModelState.IsValid)
            {
                DatabaseConnector.SavePerson(person);
            }

            return View(person);
        }

        public List<Voorstelling> GetAllVoorstellingen()
        {
            var rows = DatabaseConnector.GetRows("select * from voorstellingen");

            List<Voorstelling> voorstellingen = new List<Voorstelling>();

            foreach (var row in rows)
            {
                Voorstelling v = GetVoorstellingFromRow(row);

                voorstellingen.Add(v);
            }

            return voorstellingen;
        }


        public Voorstelling GetVoorstelling(int id)
        {
            // voorstelling ophalen uit de database op basis van het id
            var rows = DatabaseConnector.GetRows($"select * from voorstellingen where id = {id}");

            // We krijgen altijd een lijst terug maar er zou altijd één voorstelling in moeten
            // zitten dus we pakken voor het gemak gewoon de eerste
            
            Voorstelling voorstelling = GetVoorstellingFromRow(rows[0]);

            // Als laatste sturen het product uit de lijst terug
            return voorstelling;
        }


        private Voorstelling GetVoorstellingFromRow(Dictionary<string, object> row)
        {
            Voorstelling v = new Voorstelling();
            v.Name = row["name"].ToString();
            v.Desc_Short = row["descShort"].ToString();
            v.Desc_Long = row["descLong"].ToString();
            v.Age = Convert.ToInt32(row["age"]);
            v.Img = row["img"].ToString();
            v.Date = row["date"].ToString();
            v.Starting_Time = row["startingTime"].ToString();
            v.Ending_Time = row["endingTime"].ToString();
            v.Availability = Convert.ToInt32(row["availability"]);
            v.Id = Convert.ToInt32(row["id"]);

            return v;
        }

        [Route("voorstelling/{id}")]
        public IActionResult VoorstellingDetails(int id)
        {
            var voorstelling = GetVoorstelling(id);

            return View(voorstelling);
        }

        [Route("404")]
        public IActionResult Error()
        {
            return View();
        }
    }
}