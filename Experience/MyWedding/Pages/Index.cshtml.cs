using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyWedding.Common;
using MyWedding.Data;
using MyWedding.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyWedding.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public readonly ApplicationDbContext _context;
        public string FirstMale { get; set; }
        public string FirstFeMale { get; set; }
        public Wedding myWedding { get; set; }
        public WeddingCoupleModel weddingCoupleModel { get; set; }
        public List<Data.Blog> blogs { get; set; }
        public List<Data.FileUpload> albums { get; set; }
        public List<Data.WeddingVideo> videos { get; set; }
        public List<Data.Event> events { get; set; }
        public List<Data.Suggestion> suggestions { get; set; }
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            //MyWedding
            myWedding = _context.weddings.Where(x => x.Url.Contains(Request.Host.Value)).FirstOrDefault() ?? new Wedding();//Request.Host.Value
            if (myWedding == null || myWedding.ID == 0) return;
            VariableGlobal.IDWeddingGuest = myWedding.ID;
            myWedding.Image = Helper.GetPathImage(myWedding.Image);
            myWedding.ImageHome = Helper.GetPathImage(myWedding.ImageHome);
            myWedding.ImageCouple = Helper.GetPathImage(myWedding.ImageCouple);
            myWedding.ImageBlog = Helper.GetPathImage(myWedding.ImageBlog);
            myWedding.ImageAlbum = Helper.GetPathImage(myWedding.ImageAlbum);
            myWedding.ImageVideo = Helper.GetPathImage(myWedding.ImageVideo);
            myWedding.ImageEvent = Helper.GetPathImage(myWedding.ImageEvent);
            myWedding.ImageSuggestion = Helper.GetPathImage(myWedding.ImageSuggestion);
            myWedding.ImageBenefit = Helper.GetPathImage(myWedding.ImageBenefit);

            //Wedding Coupble
            weddingCoupleModel = _context.weddingCouples.Where(x => x.IDWedding == myWedding.ID).ToList().Change();
            weddingCoupleModel.MaleImage = Helper.GetPathImage(weddingCoupleModel.MaleImage);
            weddingCoupleModel.MaleImageBank = Helper.GetPathImage(weddingCoupleModel.MaleImageBank);
            weddingCoupleModel.FeMaleImage = Helper.GetPathImage(weddingCoupleModel.FeMaleImage);
            weddingCoupleModel.FeMaleImageBank = Helper.GetPathImage(weddingCoupleModel.FeMaleImageBank);
            FirstMale = weddingCoupleModel.MaleName.Split(' ')?.Last()?.Substring(0, 1)?.ToUpper();
            FirstFeMale = weddingCoupleModel.FeMaleName.Split(' ')?.Last()?.Substring(0, 1)?.ToUpper();

            //Blog
            blogs = _context.blogs.Where(x => x.IDWedding == myWedding.ID && !x.IsHidden).ToList();
            blogs.ForEach((item) =>
            {
                item.Image = Helper.GetPathImage(item.Image);
            });

            //albums
            albums = _context.fileUploads.Where(x => x.IDWedding == myWedding.ID && !x.IsHidden).ToList() ?? new List<Data.FileUpload>();
            albums.ForEach((item) =>
            {
                item.Path = Helper.GetPathImage(item.Path);
            });

            //events
            events = _context.events.Where(x => x.IDWedding == myWedding.ID).ToList() ?? new List<Data.Event>();
            events.ForEach((item) =>
            {
                item.Image = Helper.GetPathImage(item.Image);
            });

            //suggestions
            suggestions = _context.suggestions.Where(x => x.IDWedding == myWedding.ID && !x.IsHidden).ToList() ?? new List<Data.Suggestion>();

            //videos
            videos = _context.weddingVideos.Where(x => x.IDWedding == myWedding.ID).ToList() ?? new List<Data.WeddingVideo>();
        }
    }
}
