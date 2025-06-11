using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearning.Models.Dtos
{
    public class UserProfileActivities_ItemToAddDto
    {
        public int UserProfileActivitieId { get; set; }
        public int ResourceId { get; set; }
        public int Quantity { get; set; }
    }
}