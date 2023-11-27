using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWedding.Pages.Admin.Video
{
    public class IndexModel : CustPageModel
    {
        [BindProperty]
        public List<Data.WeddingVideo> weddingVideo { get; set; }
        public IndexModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet()
        {
            return IsCheckIDWedding(() =>
            {
                weddingVideo = _context.weddingVideos.Where(x => x.IDWedding == VariableGlobal.IDWedding).ToList();
            });
        }
        public async Task<IActionResult> OnGetDelete(int ID)
        {
            var item = _context.weddingVideos.Where(x => x.ID == ID).FirstOrDefault();
            if (item != null)
            {
                _context.weddingVideos.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("/");
        }
    }
}
