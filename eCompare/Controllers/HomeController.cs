using eCompare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eCompare.Controllers
{
    public class HomeController : Controller
    {
        eCompareDatabase db = new eCompareDatabase();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slide1()
        {
            var slide = db.Urun.OrderByDescending(x => x.tarih).Take(10);
            return View(slide);
        }

        public ActionResult Banner()
        {
            var banner = db.Kategori.OrderBy(x => Guid.NewGuid()).Take(3);
            return View(banner);
        }

        public ActionResult OurProduct()
        {
            var urunler = db.Urun.OrderBy(x => Guid.NewGuid()).Take(8);
            return View(urunler);
        }

        public ActionResult BannerVideo()
        {
            var bannerVideo = db.Kategori.Where(x => x.video_yol != null).OrderBy(x => Guid.NewGuid()).Take(1);
            return View(bannerVideo);
        }

        public ActionResult Header()
        {
            if (Session["kullanici_id"] != null)
            {
                int id = (int)Session["kullanici_id"];
                var kullanici = db.Kullanici.Where(x => x.id == id).FirstOrDefault();
                var sepet = db.Sepet.Where(x => x.kullanci_id == kullanici.id);
                return View(sepet);
            }
            return View();
        }

        public ActionResult Footer()
        {
            return View();
        }
        public ActionResult Giris()
        {
            return View();
        }
        public ActionResult KayitOl()
        {
            ViewBag.il_id = new SelectList(db.İl, "id", "ad");
            ViewBag.ilce_id = new SelectList(db.İlce, "id", "ad");
            return View();
        }
        [HttpPost]
        public ActionResult KayitOl(string FirstName, string LastName, string InputEmail, string InputPassword, string RepeatPassword, int il_id, int ilce_id, string InputAdress)
        {
            Kullanici yeni = new Kullanici();
            Adres yeniAdres = new Adres();
            yeniAdres.il_id = il_id;
            yeniAdres.ilce_id = ilce_id;
            yeniAdres.detay = InputAdress;
            db.Adres.Add(yeniAdres);
            db.SaveChanges();
            yeni.ad = FirstName;
            yeni.soyad = LastName;
            yeni.mail = InputEmail;
            yeni.parola = InputPassword;
            yeni.adres_id = yeniAdres.id;
            db.Kullanici.Add(yeni);
            db.SaveChanges();
            var kullanici = db.Kullanici.Where(x => x.mail == InputEmail).FirstOrDefault();
            if (kullanici != null)
            {
                if (kullanici.parola == InputPassword)
                {
                    FormsAuthentication.RedirectFromLoginPage(InputEmail, true);
                    FormsAuthentication.SetAuthCookie(kullanici.mail, true);
                    Session["kullanici_id"] = kullanici.id;
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Giris(string email, string parola, string beniHatirla, string ReturnUrl = "")
        {
            var kullanici = db.Kullanici.Where(x => x.mail == email).FirstOrDefault();
            if (kullanici != null)
            {
                if (kullanici.parola == parola)
                {
                    var bh = Convert.ToBoolean(beniHatirla);
                    FormsAuthentication.RedirectFromLoginPage(email, bh);
                    FormsAuthentication.SetAuthCookie(kullanici.mail, true);
                    Session["kullanici_id"] = kullanici.id;
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
            return View();
        }
        public ActionResult UrunSepeteEkle(int id)
        {
            if (Session["kullanici_id"] != null)
            {
                int kullaninici_id = (int)Session["kullanici_id"];
                var kullanici = db.Kullanici.Where(x => x.id == kullaninici_id).FirstOrDefault();
                var sepeteEkliMi = db.Sepet.Where(x => x.kullanci_id == kullanici.id && x.urun_id == id).FirstOrDefault();
                if (sepeteEkliMi == null)
                {
                    Sepet sepetUrun = new Sepet();
                    sepetUrun.kullanci_id = kullanici.id;
                    sepetUrun.urun_adet = 1;
                    sepetUrun.urun_id = id;
                    db.Sepet.Add(sepetUrun);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Sepet");
                }
                else
                {
                    sepeteEkliMi.urun_adet += 1;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Sepet");
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult SepettenUrunSil(int id)
        {
            db.Sepet.Remove(db.Sepet.FirstOrDefault(x => x.id == id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}