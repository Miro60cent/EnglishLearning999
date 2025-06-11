using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishLearning.Api.Entities
{
    public class UserProfileActivitie_Item
    {
        public int Id { get; set; }
        public int UserProfileActivitieId { get; set; }
        public int ResourceId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("UserProfileActivitieId")]
        public UserProfileActivitie UserProfileActivitie { get; set; }

        [ForeignKey("ResourceId")]
        public Resource Resource { get; set; }
    }
}