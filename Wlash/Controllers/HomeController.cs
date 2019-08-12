using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wlash.Models;

namespace Wlash.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArticleContext article;
        private IEnumerable<ArticleModel> listArt;

        public HomeController(ArticleContext article)
        {
            this.article = article;
            listArt = new List<ArticleModel>();
        }

        public IActionResult Index()
        {
            listArt = article.Articles.ToList().Reverse<ArticleModel>();
            listArt = listArt.Select(cont => {
                cont.Content = cont.Content.Substring(0, 700) + " ...";

                return cont;
            });
            
            return View(listArt);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddArticle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> About(ArticleModel art)
        {
            await article.AddAsync(art);
            await article.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
