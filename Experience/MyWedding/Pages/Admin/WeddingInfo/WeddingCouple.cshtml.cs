using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using MyWedding.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace MyWedding.Pages.Admin.WeddingInfo
{
    public class WeddingCouple : CustPageModel
    {
        [BindProperty]
        public WeddingCoupleModel weddingCouple { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Chọn file upload")]
        [BindProperty]
        public IFormFile MaleFileUpload { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Chọn file upload")]
        [BindProperty]
        public IFormFile FeMaleFileUpload { get; set; }

        public WeddingCouple(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet()
        {
            return IsCheckIDWedding(() =>
            {
                weddingCouple = _context.weddingCouples.Where(x => x.IDWedding == VariableGlobal.IDWedding).ToList()?.Change();
            });
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (MaleFileUpload != null)
                    {
                        weddingCouple.MaleImageBank = Guid.NewGuid().ToString() + new FileInfo(MaleFileUpload.FileName).Extension;
                        var file = Path.Combine(Helper.RootImage(), weddingCouple.MaleImageBank);
                        using (var fileStream = new FileStream(file, FileMode.Create))
                        {
                            MaleFileUpload.CopyTo(fileStream);
                        }
                    }

                    if (FeMaleFileUpload != null)
                    {
                        weddingCouple.FeMaleImageBank = Guid.NewGuid().ToString() + new FileInfo(FeMaleFileUpload.FileName).Extension;
                        var file = Path.Combine(Helper.RootImage(), weddingCouple.FeMaleImageBank);
                        using (var fileStream = new FileStream(file, FileMode.Create))
                        {
                            FeMaleFileUpload.CopyTo(fileStream);
                        }
                    }
                    var weddings = _context.weddingCouples.Where(x => x.IDWedding.Equals(weddingCouple.IDWedding)).ToList() ?? new System.Collections.Generic.List<Data.WeddingCouple>();
                    weddings = weddingCouple.Change(weddings);
                    foreach (var item in weddings)
                    {
                        if (item.ID == 0)
                            _context.weddingCouples.Add(item);
                        else
                            _context.weddingCouples.Update(item);
                    }
                    _context.SaveChanges();

                    return Redirect("/Admin/WeddingInfo/Index");
                }
                catch
                {

                }

            }
            return Page();
        }
    }
}
