namespace RedWizards.Database
{
    public class Voorstelling
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Desc_Short { get; set; }

        public string? Desc_Long { get; set; }

        public int Age { get; set; }

        public string? Img { get; set; }

        public DateTime Date { get; set; }

        public string? Starting_Time { get; set; }

        public string? Ending_Time { get; set; }

        public int Availability { get; set; }
    }
}
