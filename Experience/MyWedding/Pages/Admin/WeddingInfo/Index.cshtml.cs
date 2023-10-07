using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System.Linq;

namespace MyWedding.Pages.Admin.WeddingInfo
{
    public class IndexModel : CustPageModel
    {
        [BindProperty]
        public Wedding weddingInfo { get; set; }

        public IndexModel(ApplicationDbContext context) : base(context)
        {

        }
        public void OnGet()
        {
            weddingInfo = _context.weddings.Where(x => x.UserName.Equals(this.User.Identity.Name)).FirstOrDefault() ?? new Wedding();
            VariableGlobal.IDWedding = weddingInfo.ID;
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var wedding = _context.weddings.Where(x => x.UserName.Equals(this.User.Identity.Name)).FirstOrDefault() ?? new Wedding() { UserName = this.User.Identity.Name };
                    wedding.Name = weddingInfo.Name;
                    wedding.StartDate = weddingInfo.StartDate;
                    wedding.Url = weddingInfo.Url;
                    wedding.WeddingFaMale = weddingInfo.WeddingFaMale;
                    wedding.WeddingMale = weddingInfo.WeddingMale;
                    wedding.Image = weddingInfo.Image;
                    if (wedding.ID == 0)
                    {
                        _context.weddings.Add(wedding);
                    }
                    else
                    {
                        _context.weddings.Update(wedding);
                    }
                    _context.SaveChanges();

                    VariableGlobal.IDWedding = wedding.ID;

                    return Redirect("/Admin/WeddingInfo/WeddingCouple");
                }
                catch
                {

                }
            }
            return Page();
        }
    }
}
