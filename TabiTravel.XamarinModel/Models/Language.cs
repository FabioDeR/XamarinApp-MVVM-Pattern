using System.ComponentModel.DataAnnotations;

namespace TabiTravel.XamarinModel.Models
{
    public class Language
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(3)]
        public string Abbreviation { get; set; }
    }
}