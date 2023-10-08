using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System;
using System.Linq;

namespace MyWedding.Pages.Admin.Video
{
    public class DetailsModel : CustPageModel
    {
        [BindProperty]
        public Data.WeddingVideo weddingVideo { get; set; }

        public DetailsModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet(int ID)
        {
            return IsCheckIDWedding(() =>
            {
                if (ID > 0)
                {
                    weddingVideo = _context.weddingVideos.Where(x => x.ID == ID).FirstOrDefault() ?? new Data.WeddingVideo();
                }
                else
                {
                    weddingVideo = new Data.WeddingVideo();
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
                        weddingVideo.DateUpload = DateTime.Now.ToString("yyyy-MM-dd");

                        if (weddingVideo.ID == 0)
                        {
                            weddingVideo.IDWedding = VariableGlobal.IDWedding;
                            _context.weddingVideos.Add(weddingVideo);
                        }
                        else
                        {
                            _context.weddingVideos.Update(weddingVideo);
                        }
                        _context.SaveChanges();
                    }, "/Admin/Video/Index");
                }
                catch
                {

                }
            }
            return Page();
        }
    }
}
