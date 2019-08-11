using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wlash.Models;

namespace Wlash.Controllers
{
    public class CompleteArticleController : Controller
    {
        private readonly ArticleContext article;

        public CompleteArticleController(ArticleContext article)
        {
            this.article = article;
        }


        public IActionResult Index(string id)
        {
            var result = article.Articles.Where(s => s.Title == id).ToList();
            return View(result);
        }
    }
}