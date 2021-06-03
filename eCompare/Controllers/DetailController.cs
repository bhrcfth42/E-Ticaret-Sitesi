using eCompare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCompare.Controllers
{
    public class DetailController : Controller
    {
        eCompareDatabase db = new eCompareDatabase();
        // GET: Detail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Urun(int id)
        {
            var urunDetail = db.Urun.Where(x => x.id == id).FirstOrDefault();
            ViewBag.Title = urunDetail.adi;
            return View(urunDetail);
        }
        public ActionResult RelatedUrun()
        {
            var relatedUrunler = db.Urun.OrderBy(x => Guid.NewGuid()).Take(8);
            return View(relatedUrunler);
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