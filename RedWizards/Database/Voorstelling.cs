namespace RedWizards.Database
{
    public class Voorstelling
    {
        public int Id { get; set; }

        public string? Categorie { get; set; }

        public string? Naam { get; set; }

        public string? Datum { get; set; }

        public string? Beschrijving_Kort { get; set; }

        public string? Beschrijving_Lang { get; set; }

        public string? Image { get; set; }

        public string? Minleeftijd { get; set; }

        public string? Prijs_1erang { get; set; }

        public string? Prijs_2erang { get; set; }

        public string? Prijs_3erang { get; set; }
    }
}
