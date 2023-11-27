using Microsoft.AspNetCore.Mvc;
using MyWedding.Common;
using MyWedding.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWedding.Pages.Admin.GuestConnect
{
    public class IndexModel : CustPageModel
    {
        [BindProperty]
        public List<Data.GuestConnect> guestConnects { get; set; }
        public IndexModel(ApplicationDbContext context) : base(context)
        {

        }
        public IActionResult OnGet()
        {
            return IsCheckIDWedding(() =>
            {
                guestConnects = _context.guestConnects.Where(x => x.IDWedding == VariableGlobal.IDWedding).OrderByDescending(x => x.ID).ToList();
            });
        }
        public JsonResult OnGetUpdateToken()
        {
            var token = string.Empty;
            try
            {
                var connect = HttpContext.Connection;
                var temp = new Data.GuestConnect()
                {
                    IDWedding = VariableGlobal.IDWeddingGuest,
                    Token = connect.Id.ToString(),
                    RemoteIpAddress = connect.RemoteIpAddress.ToString(),
                    RemotePort = connect.RemotePort.ToString(),
                    DateConnect = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };

                if (!string.IsNullOrWhiteSpace(temp.Token) && !_context.guestConnects.Any(x => x.Token.Equals(temp.Token)))
                {
                    _context.guestConnects.Add(temp);
                    _context.SaveChanges();
                }
                token = temp.Token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new JsonResult(token);
        }
    }
}
