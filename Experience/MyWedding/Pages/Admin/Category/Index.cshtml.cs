using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWedding.Pages.Admin.Category
{
    public class IndexModel : CustPageModel
    {
        [BindProperty]
        public List<Data.Category> weddingCategories { get; set; }
        public IndexModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet()
        {
            return IsCheckIDWedding(() =>
            {
                weddingCategories = _context.categories.Where(x => x.IDWedding == VariableGlobal.IDWedding).ToList();
                weddingCategories.ForEach((item) =>
                {
                    item.Path = Helper.GetPathImage(item.Path);
                });
            });
        }
        public async Task<IActionResult> OnGetDelete(int ID)
        {
            var item = _context.categories.Where(x => x.ID == ID).FirstOrDefault();
            if (item != null)
            {
                _context.categories.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("/");
        }
        public async Task<IActionResult> OnGetHidden(int ID)
        {
            var item = _context.categories.Where(x => x.ID == ID).FirstOrDefault();
            if (item != null && !item.IsHidden)
            {
                item.IsHidden = true;
                _context.categories.Update(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("/");
        }
    }
}
