using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.GiamKichSan;
using Common.GiamKichSan.BaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace admim.GiamKichSan.Pages.Dialog
{
    public class FilesListModel : PageModel
    {
        private string rootDirection = Path.Combine("wwwroot", "filesImport");
        public List<ItemCollectionEntity> directories { get; set; }
        public void OnGet()
        {
            directories = CommonDirectory.GetDirection(Path.Combine(Environment.CurrentDirectory, rootDirection));
        }
        public void OnPost(string name)
		{
            CommonDirectory.Create(Path.Combine(Environment.CurrentDirectory, "wwwroot", "filesImport", name));
            directories = CommonDirectory.GetDirection(Path.Combine(Environment.CurrentDirectory, rootDirection));
        }
        public void CreateFile()
		{

		}
    }
}
