using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearning.Models.Dtos
{
    public class UserProfileActivities_ItemDto
    {
        public int Id { get; set; }
        public int UserProfileActivitiesId { get; set; }
        public int ResourceId { get; set; }
        public string Model_Name { get; set; }
        public int Topic_id { get; set; }
        public string Topic_Name { get; set; }
        public string Topic_Avatar_Src { get; set; }
        public string Color { get; set; }
        public string Image_Src { get; set; }
        public bool In_Stock { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}