using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWedding.Pages.Admin.Suggestion
{
    public class IndexModel : CustPageModel
    {
        [BindProperty]
        public List<Data.Suggestion> suggestions { get; set; }
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
            if (item != null)
            {
                item.IsHidden = !item.IsHidden;
                _context.suggestions.Update(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("/");
        }
        public JsonResult OnGetUpdate()
        {
            try
            {
                var suggestionTitle = Convert.ToString(Request.Query["SuggestionTitle"]);
                var suggestionContent = Convert.ToString(Request.Query["SuggestionContent"]);
                var temp = new Data.Suggestion()
                {
                    IDWedding = VariableGlobal.IDWeddingGuest,
                    CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Title = suggestionTitle,
                    Content = suggestionContent
                };

                _context.suggestions.Add(temp);
                _context.SaveChanges();
                return new JsonResult("{status:True}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("{status:False}");
        }
    }
}
