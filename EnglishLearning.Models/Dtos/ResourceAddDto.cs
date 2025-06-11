using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearning.Models.Dtos
{ 
    public class ResourceAddDto
    {
        public string Model_Name { get; set; }
        public int Topic_id { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string Image_Src { get; set; }
        public string Form_factor { get; set; }
        public bool Wireless { get; set; }
        public bool Noice_Cancellation { get; set; }
        public bool Waterproof { get; set; }
        public bool Microphone { get; set; }
        public bool In_Stock { get; set; }
    }
}