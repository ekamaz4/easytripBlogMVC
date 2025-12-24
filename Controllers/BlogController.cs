using Microsoft.AspNetCore.Mvc;
using easy_trip.Models.Siniflar;
using NuGet.Packaging.Signing;
using Microsoft.EntityFrameworkCore;
namespace easy_trip.Controllers
{
    public class BlogController : Controller
    {
        private readonly Context c;
        BlogYorum by = new BlogYorum();
        public BlogController(Context context)
        {
            c = context;
        }
        public IActionResult Index()
        {
            by.Deger1 = c.Blogs.ToList();
            by.Deger3= c.Blogs.OrderByDescending(x=>x.ID).Take(10).ToList();
            by.SonYorumDegerler = c.Yorumlars.OrderByDescending(x => x.ID).Take(5).ToList();
            return View(by);
        }
        public IActionResult BlogDetay(int id) {
            by.Deger1 = c.Blogs.Where(x => x.ID == id).ToList();
            by.Deger2 = c.Yorumlars.Where(y => y.Blogid == id).ToList();
            by.Deger3 = c.Blogs.OrderByDescending(x => x.ID).Take(10).ToList();
            by.SonYorumDegerler = c.Yorumlars.OrderByDescending(x => x.ID).Take(5).ToList();
            return View(by);  
        }
        [HttpGet]
        public PartialViewResult YorumYap(int id)
        {
            ViewBag.deger = id;
            return PartialView();
        }
        [HttpPost]
        public IActionResult YorumYap(Yorumlar y)
        {
            c.Yorumlars.Add(y);
            c.SaveChanges();
            return RedirectToAction("BlogDetay", "Blog", new { id = y.Blogid });
        }
    }
}
