using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic.Models.Dtos
{
	public class UserAddDto
    {
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
