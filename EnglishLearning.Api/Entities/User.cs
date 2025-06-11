namespace EnglishLearning.Api.Entities
{
    public class User
    {
		public int Id { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Telephone { get; set; }
		public string Avatar_Src { get; set; }
		public int Discount_Percent { get; set; }
		public bool Is_Admin { get; set; }
    }
}