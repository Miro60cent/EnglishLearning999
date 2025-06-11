using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearning.Models.Dtos
{
	public class UserUpdateDto
	{
		public string FullName { get; set; }
		public string Password { get; set; }
		public string Telephone { get; set; }
		public string Avatar_Src { get; set; }
		public int Discount_Percent { get; set; }
		public bool Is_Admin { get; set; }
	}
}