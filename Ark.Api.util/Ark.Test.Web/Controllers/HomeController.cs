using Ark.Test.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ark.Test.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var r1 = ark.net.util.EmailUtil.IsValidFormat("ewewe");
            var r2 = ark.net.util.EmailUtil.IsValidFormat("ewewe@www.cc");
            var r3 = ark.net.util.EmailUtil.IsValidFormat("raj@immanuel.co");
            var r4 = ark.net.util.EmailUtil.IsValidFormat("www@jjjj.kk  ");
            var r5 = ark.net.util.EmailUtil.IsValidFormat("  ww  w@jj  jj.kk  ");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<dynamic> TestHttpPostForm()
        {
            return await ark.net.util.HttpUtil.PostForm("https://reqbin.com/echo/post/form", new Dictionary<string, string>() { { "k1", "v1" } });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
