using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace admim.GiamKichSan.Base
{
	public class BasePageModel: PageModel
	{
		public string currentDirectory = Directory.GetCurrentDirectory();

	}
}
