using eCompare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCompare.Controllers
{
    public class OdemeController : Controller
    {
        eCompareDatabase db = new eCompareDatabase();
        // GET: Odeme
        public ActionResult Index()
        {
            if (Session["kullanici_id"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        public ActionResult Header()
        {
            return View();
        }
        public ActionResult Footer()
        {
            return View();
        }
        public ActionResult SepetKismi()
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
        public ActionResult AdresKismi()
        {
            ViewBag.il_id = new SelectList(db.İl, "id", "ad");
            ViewBag.ilce_id = new SelectList(db.İlce, "id", "ad");
            return View();
        }
        public ActionResult OdemeKismi()
        {
            return View();
        }
        public ActionResult SepettenUrunSil(int id)
        {
            db.Sepet.Remove(db.Sepet.FirstOrDefault(x => x.id == id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SiparisKayit()
        {
            if (Session["kullanici_id"] != null)
            {
                int id = (int)Session["kullanici_id"];
                var kullanici = db.Kullanici.Where(x => x.id == id).FirstOrDefault();
                var sepet = db.Sepet.Where(x => x.kullanci_id == kullanici.id);
                foreach (var urun in sepet)
                {
                    Siparis siparis = new Siparis();
                    siparis.kullanici_id = kullanici.id;
                    siparis.urun_id = urun.urun_id;
                    siparis.adres_id = kullanici.adres_id;
                    siparis.adet = urun.urun_adet;
                    siparis.tarih = DateTime.Now;
                    db.Siparis.Add(siparis);
                    db.Sepet.Remove(urun);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
            
        }
    }
}