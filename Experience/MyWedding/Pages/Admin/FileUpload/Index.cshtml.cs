using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using MyWedding.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyWedding.Pages.Admin.FileUpload
{
    public class IndexModel : CustPageModel
    {
        [BindProperty]
        public List<FileUploadModel> weddingFileUpload { get; set; }
        public IndexModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet()
        {
            return IsCheckIDWedding(() =>
            {
                weddingFileUpload = _context.fileUploads.Where(x => x.IDWedding == VariableGlobal.IDWedding).Select(x => x.Change()).ToList() ?? new List<FileUploadModel>();
            });
        }
        public async Task<IActionResult> OnGetDelete(int ID)
        {
            var item = _context.fileUploads.Where(x => x.ID == ID).FirstOrDefault();
            if (item != null)
            {
                _context.fileUploads.Remove(item);
                await _context.SaveChangesAsync();
                if (!string.IsNullOrEmpty(item.Path))
                {
                    System.IO.File.Delete(Path.Combine(Helper.RootImage(), item.Path));
                }
            }
            return RedirectToAction("/");
        }
    }
}
