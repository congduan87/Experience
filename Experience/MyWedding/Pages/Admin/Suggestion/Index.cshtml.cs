using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWedding.Pages.Admin.Suggestion
{
    public class IndexModel : CustPageModel
    {
        [BindProperty]
        public List<MyWedding.Data.Suggestion> suggestions { get; set; }
        public IndexModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet()
        {
            return IsCheckIDWedding(() =>
            {
                suggestions = _context.suggestions.Where(x => x.IDWedding == VariableGlobal.IDWedding).ToList();
            });
        }
        public async Task<IActionResult> OnGetHidden(int ID)
        {
            var item = _context.suggestions.Where(x => x.ID == ID).FirstOrDefault();
            if (item != null && !item.IsHidden)
            {
                item.IsHidden = true;
                _context.suggestions.Update(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("/");
        }
    }
}
