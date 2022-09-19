using System;
using System.Collections.Generic;
using System.Text;

namespace TabiTravel.XamarinModel.Models
{
    public class Country
    {
        public string Name { get; set; }

        public Language Language { get; set; }
       
        public int LanguageId { get; set; }
    }
}
