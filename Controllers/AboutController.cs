using Microsoft.AspNetCore.Mvc;
using easy_trip.Models.Siniflar;

namespace easy_trip.Controllers
{
    public class AboutController : Controller
    {
        private readonly Context c;
        public AboutController(Context context)
        {
            c = context;
        }
        public IActionResult Index()
        {
            var degerler = c.Hakkimizdas.ToList();
            return View(degerler);
        }
    }
}
