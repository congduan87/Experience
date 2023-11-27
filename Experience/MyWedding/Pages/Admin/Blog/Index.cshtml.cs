using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWedding.Pages.Admin.Blog
{
    public class IndexModel : CustPageModel
    {
        [BindProperty]
        public List<Data.Blog> weddingBlog { get; set; }
        public IndexModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet()
        {
            return IsCheckIDWedding(() =>
            {
                weddingBlog = _context.blogs.Where(x => x.IDWedding == VariableGlobal.IDWedding).ToList();
                weddingBlog.ForEach((item) =>
                {
                    item.Image = Helper.GetPathImage(item.Image);
                });
            });
        }
        public async Task<IActionResult> OnGetDelete(int ID)
        {
            var item = _context.blogs.Where(x => x.ID == ID).FirstOrDefault();
            if (item != null)
            {
                _context.blogs.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("/");
        }
    }
}
