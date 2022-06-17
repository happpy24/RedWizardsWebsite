using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RedWizards.Database;
using RedWizards.Models;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

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
            ViewData["user"] = HttpContext.Session.GetString("User");
            var voorstellingen = GetAllVoorstellingen();

            return View(voorstellingen);
        }

        // █▄▄ ▄▀█ █▀ █ █▀▀
        // █▄█ █▀█ ▄█ █ █▄▄

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

        // █░░ █▀█ █▀▀ █ █▄░█
        // █▄▄ █▄█ █▄█ █ █░▀█

        [Route("login")]
        public IActionResult Login(string username, string password)
        {
            // hash voor 'wachtwoord'
            string hash = "dc00c903852bb19eb250aeba05e534a6d211629d77d055033806b783bae09937";

            // is er een wachtwoord ingevoerd?
            if (!string.IsNullOrWhiteSpace(password))
            {

                //Er is iets ingevoerd, nu kunnen we het wachtwoord hashen en vergelijken met de hash "uit de database"
                string hashVanIngevoerdWachtwoord = ComputeSha256Hash(password);
                if (hashVanIngevoerdWachtwoord == hash)
                {
                    HttpContext.Session.SetString("User", username);
                    return Redirect("/");
                }
            }
            return View();
        }

        // █▀▀ █▀█ █▄░█ ▀█▀ ▄▀█ █▀▀ ▀█▀
        // █▄▄ █▄█ █░▀█ ░█░ █▀█ █▄▄ ░█░

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

        // █░█ █▀█ █▀█ █▀█ █▀ ▀█▀ █▀▀ █░░ █░░ █ █▄░█ █▀▀
        // ▀▄▀ █▄█ █▄█ █▀▄ ▄█ ░█░ ██▄ █▄▄ █▄▄ █ █░▀█ █▄█

        public List<Voorstelling> GetAllVoorstellingen()
        {
            var rows = DatabaseConnector.GetRows("SELECT voorstellingdata.id, voorraad, name, datum, descShort, descLong, begintijd, eindtijd, img, age, voorstelling_id FROM `voorstellingdata` INNER JOIN voorstellingen ON voorstellingdata.voorstelling_id = voorstellingen.id");

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
            var rows = DatabaseConnector.GetRows($"SELECT voorstellingdata.id, voorraad, name, datum, descShort, descLong, begintijd, eindtijd, img, age, voorstelling_id FROM `voorstellingdata` INNER JOIN voorstellingen ON voorstellingdata.voorstelling_id = voorstellingen.id WHERE voorstellingen.id = {id}");

            // We krijgen altijd een lijst terug maar er zou altijd één voorstelling in moeten
            // zitten dus we pakken voor het gemak gewoon de eerste
            if (!rows.Any())
                return null;

            var row = rows[0];

            // lijst maken om alle producten in te stoppen
            Voorstelling v = new Voorstelling();
            v.Name = row["name"].ToString();
            v.Desc_Short = row["descShort"].ToString();
            v.Desc_Long = row["descLong"].ToString();
            v.Datum = row["datum"].ToString();
            v.Availability = Convert.ToInt32(row["voorraad"]);
            v.Starting_Time = row["begintijd"].ToString();
            v.Ending_Time = row["eindtijd"].ToString();
            v.Age = Convert.ToInt32(row["age"]);
            v.Id = Convert.ToInt32(row["id"]);
            v.Img = row["img"].ToString();

            // Als laatste sturen het product uit de lijst terug
            return v;
        }

        private Voorstelling GetVoorstellingFromRow(Dictionary<string, object> row)
        {
            Voorstelling v = new Voorstelling();
            v.Name = row["name"].ToString();
            v.Desc_Short = row["descShort"].ToString();
            v.Desc_Long = row["descLong"].ToString();
            v.Age = Convert.ToInt32(row["age"]);
            v.Img = row["img"].ToString();
            v.Datum = row["datum"].ToString();
            v.Starting_Time = row["begintijd"].ToString();
            v.Ending_Time = row["eindtijd"].ToString();
            v.Availability = Convert.ToInt32(row["voorraad"]);
            v.Id = Convert.ToInt32(row["id"]);

            return v;
        }

        [Route("voorstelling/{id}")]
        public IActionResult VoorstellingDetails(int id)
        {
            var voorstelling = GetVoorstelling(id);

            if(voorstelling == null)
                return Error();

            return View(voorstelling);
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [Route("404")]
        public IActionResult Error()
        {
            return View();
        }
    }
}