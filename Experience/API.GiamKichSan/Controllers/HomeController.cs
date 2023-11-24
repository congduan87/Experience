using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.GiamKichSan.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult MyIp()
        {
            return View();
        }
    }
}
