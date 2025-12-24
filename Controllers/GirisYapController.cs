using easy_trip.Models.Siniflar;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
namespace easy_trip.Controllers
{

    public class GirisYapController : Controller
    {
        private readonly Context c;
        public GirisYapController(Context context)
        {
           c = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            var bilgiler = c.Admins.FirstOrDefault(
                x => x.Kullanici == admin.Kullanici 
                && 
                x.Sifre == admin.Sifre);

            if(bilgiler != null)    
            {
                var kullaniciId = bilgiler.ID;
                HttpContext.Session.SetInt32("Kullanici", kullaniciId);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, admin.Kullanici)
                };
                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Kullanici");
            return RedirectToAction("Login", "GirisYap");
        }

    }

}
