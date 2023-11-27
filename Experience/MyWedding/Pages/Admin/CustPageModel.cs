using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWedding.Common;
using MyWedding.Data;
using System;

namespace MyWedding.Pages.Admin
{
    public class CustPageModel : PageModel
    {
        public readonly ApplicationDbContext _context;
        public CustPageModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult IsCheckIDWedding(Action method, string url = null)
        {
            if (VariableGlobal.IDWedding == 0)
            {
                return Redirect("/Admin/WeddingInfo/Index");
            }
            method();
            if (string.IsNullOrEmpty(url))
                return Page();
            else
                return Redirect(url);
        }
    }
}
