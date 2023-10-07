using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System;
using System.Linq;

namespace MyWedding.Pages.Admin.Event
{
    public class DetailsModel : CustPageModel
    {
        [BindProperty]
        public Data.Event weddingEvent { get; set; }

        public DetailsModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet(int ID)
        {
            return IsCheckIDWedding(() =>
            {
                if (ID == 0)
                {
                    weddingEvent = new Data.Event();
                }
                else
                {
                    weddingEvent = _context.events.Where(x => x.ID == ID).FirstOrDefault() ?? new Data.Event();
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
                        weddingEvent.StartDate += Request.Form["StartDate"].ToString() + " " + Request.Form["StartDateTime"].ToString();

                        if (weddingEvent.ID == 0)
                        {
                            weddingEvent.IDWedding = VariableGlobal.IDWedding;
                            _context.events.Add(weddingEvent);
                        }
                        else
                        {
                            _context.events.Update(weddingEvent);
                        }
                        _context.SaveChanges();
                    }, "/Admin/Event/Index");
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
