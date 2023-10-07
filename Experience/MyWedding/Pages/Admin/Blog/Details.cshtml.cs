using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System.Linq;

namespace MyWedding.Pages.Admin.Blog
{
    public class DetailsModel : CustPageModel
    {
        [BindProperty]
        public Data.Blog weddingBlog { get; set; }

        public DetailsModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet(int ID)
        {
            return IsCheckIDWedding(() =>
            {
                if (ID > 0)
                {
                    weddingBlog = _context.blogs.Where(x => x.ID == ID).FirstOrDefault() ?? new Data.Blog();
                }
                else
                {
                    weddingBlog = new Data.Blog();
                }

            });
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return IsCheckIDWedding(() =>
                    {

                        if (weddingBlog.ID == 0)
                        {
                            weddingBlog.IDWedding = VariableGlobal.IDWedding;
                            _context.blogs.Add(weddingBlog);
                        }
                        else
                        {
                            _context.blogs.Update(weddingBlog);
                        }
                        _context.SaveChanges();
                    }, "/Admin/Blog/Index");
                }
                catch
                {

                }
            }
            return Page();
        }
    }
}
