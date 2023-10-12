using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System;
using System.Linq;

namespace MyWedding.Pages.Admin.WeddingInfo
{
    public class ImageCategoryModel : CustPageModel
    {
        [BindProperty]
        public Data.Wedding wedding { get; set; }

        public ImageCategoryModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet()
        {
            return IsCheckIDWedding(() =>
            {
                wedding = _context.weddings.Where(x => x.ID == VariableGlobal.IDWedding).FirstOrDefault() ?? new Data.Wedding();
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
                        var temp = _context.weddings.Where(x => x.ID == VariableGlobal.IDWedding).FirstOrDefault();
                        if (temp != null)
                        {
                            temp.Image = wedding.Image;
                            temp.ImageHome = wedding.ImageHome;
                            temp.ImageCouple = wedding.ImageCouple;
                            temp.ImageBlog = wedding.ImageBlog;
                            temp.ImageAlbum = wedding.ImageAlbum;
                            temp.ImageVideo = wedding.ImageVideo;
                            temp.ImageEvent = wedding.ImageEvent;
                            temp.ImageSuggestion = wedding.ImageSuggestion;
                            temp.ImageBenefit = wedding.ImageBenefit;

                            _context.weddings.Update(temp);
                            _context.SaveChanges();
                        }
                        _context.SaveChanges();
                    }, "/Admin/WeddingInfo/Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Page();
        }
    }
}
