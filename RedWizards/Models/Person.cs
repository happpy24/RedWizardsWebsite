using System.ComponentModel.DataAnnotations;

namespace RedWizards.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Gelieve uw naam in te vullen")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Gelieve uw email in te vullen")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Zipcode { get; set; }
        [Required(ErrorMessage = "Je moet een onderwerp hebben")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Je kan geen leeg bericht sturen")]
        public string Message { get; set; }
    }
}
