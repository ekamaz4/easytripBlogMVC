using Microsoft.AspNetCore.Mvc;
using easy_trip.Models.Siniflar;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
namespace easy_trip.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        [AllowedValues]
        public readonly Context c;
        public AdminController(Context context)
        {
            c = context;
        }
        public IActionResult Index()
        {
            var degerler = c.Blogs.ToList();
            return View(degerler);
        }
        [HttpGet]
        public IActionResult YeniBlog()
        {
            return View();
        }
        [HttpPost]
        public IActionResult YeniBlog(Blog p)
        {
            p.Tarih = DateTime.SpecifyKind(p.Tarih, DateTimeKind.Utc);
            c.Blogs.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult BlogSil(int id)
        {

            var b = c.Blogs.Find(id);
            c.Blogs.Remove(b);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult BlogGetir(int id)
        {
            var b = c.Blogs.Find(id);
            return View("BlogGetir", b);
        }
        public IActionResult HakkimizdaGetir()
        {
            var hakk = c.Hakkimizdas.FirstOrDefault();
            return View(hakk);
        }
        public IActionResult HakkimizdaGuncelle(Hakkimizda h)
        {
            var hakk = c.Hakkimizdas.SingleOrDefault(h => h.ID == h.ID);
            hakk.Aciklama = h.Aciklama;
            hakk.FotoUrl = h.FotoUrl;
            c.SaveChanges();
            return RedirectToAction("HakkimizdaGetir");
        }


        public IActionResult BlogGuncelle(Blog b)
        {
            var blg = c.Blogs.Find(b.ID);

            blg.Aciklama = b.Aciklama;
            blg.Baslik = b.Baslik;
            blg.BlogImage = b.BlogImage;
            blg.Tarih = b.Tarih;
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult YorumListesi(Blog b)
        {
            var yorumlar = c.Yorumlars.ToList();
            return View(yorumlar);
        }

        public IActionResult YorumSil(int id)
        {
            var y = c.Yorumlars.Find(id);
            c.Yorumlars.Remove(y);
            c.SaveChanges();
            return RedirectToAction("YorumListesi");
        }

        public IActionResult YorumGetir(int id)
        {
            var yr = c.Yorumlars.Find(id);
            return View("YorumGetir", yr);
        }
        public IActionResult YorumGuncelle(Yorumlar y)
        {
            var yrm = c.Yorumlars.Find(y.ID);
            yrm.KullaniciAdi = y.KullaniciAdi;
            yrm.MailAdres = y.MailAdres;
            yrm.Yorum = y.Yorum;
            c.SaveChanges();
            return RedirectToAction("YorumListesi");
        }
    }
}
