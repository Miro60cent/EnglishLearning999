using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishLearning.Api.Entities
{
    public class UserProfileActivitie
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Paid { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}