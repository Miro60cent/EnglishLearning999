using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearning.Models.Dtos
{
    public class UserProfileActivitieDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Paid { get; set; }
    }
}
