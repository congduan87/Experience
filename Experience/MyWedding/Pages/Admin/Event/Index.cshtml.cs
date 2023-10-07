using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWedding.Pages.Admin.Event
{
    public class IndexModel : CustPageModel
    {
        [BindProperty]
        public List<Data.Event> weddingEvent { get; set; }
        public IndexModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet()
        {
            return IsCheckIDWedding(() =>
            {
                weddingEvent = _context.events.Where(x => x.IDWedding == VariableGlobal.IDWedding).ToList();
                weddingEvent.ForEach((item) =>
                {
                    item.Image = Helper.GetPathImage(item.Image);
                });
            });
        }
        public async Task<IActionResult> OnGetDelete(int ID)
        {
            var item = _context.events.Where(x => x.ID == ID).FirstOrDefault();
            if (item != null)
            {
                _context.events.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("/");
        }
    }
}
