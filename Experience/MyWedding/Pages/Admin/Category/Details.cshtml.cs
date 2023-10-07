using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System.Linq;

namespace MyWedding.Pages.Admin.Category
{
    public class DetailsModel : CustPageModel
    {
        [BindProperty]
        public Data.Category weddingCategory { get; set; }

        public DetailsModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet(int ID)
        {
            return IsCheckIDWedding(() =>
            {
                if (ID > 0)
                {
                    weddingCategory = _context.categories.Where(x => x.ID == ID).FirstOrDefault() ?? new Data.Category();
                }
                else
                {
                    weddingCategory = new Data.Category();
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

                        if (weddingCategory.ID == 0)
                        {
                            weddingCategory.IDWedding = VariableGlobal.IDWedding;
                            _context.categories.Add(weddingCategory);
                        }
                        else
                        {
                            _context.categories.Update(weddingCategory);
                        }
                        _context.SaveChanges();
                    }, "/Admin/Category/Index");
                }
                catch
                {

                }
            }
            return Page();
        }
    }
}
