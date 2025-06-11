using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishLearning.Api.Entities
{
    public class Resource
    {
        public int Id { get; set; }
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

        [ForeignKey("Topic_id")]
        public Topic Topic { get; set; }
    }
}