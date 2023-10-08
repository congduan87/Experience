using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyWedding.Common;
using MyWedding.Data;

namespace MyWedding.Pages.Admin
{
    public class IndexModel : CustPageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context) : base(context)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var weddingInfo = _context.weddings.Where(x => x.UserName.Equals(this.User.Identity.Name)).FirstOrDefault() ?? new Wedding();
            if(weddingInfo != null)
            {
                VariableGlobal.IDWedding = weddingInfo.ID;
            }    
        }
    }
}
