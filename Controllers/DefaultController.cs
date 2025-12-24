using Microsoft.AspNetCore.Mvc;
using easy_trip.Models.Siniflar;
namespace easy_trip.Controllers
{
    public class DefaultController : Controller
    {
        public readonly Context c;
        public DefaultController(Context context)
        {
            c = context;
        }
        public IActionResult Index()
        {
            var degerler = c.Blogs.Take(8).ToList();
            return View(degerler);
        }
        public IActionResult About()    
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}
