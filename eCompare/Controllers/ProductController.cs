using eCompare.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCompare.Controllers
{
    public class ProductController : Controller
    {
        eCompareDatabase db = new eCompareDatabase();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TitlePage()
        {
            return View();
        }
        public ActionResult FilterPage()
        {
            return View();
        }
        public ActionResult OrderByPage()
        {
            return View();
        }
        public ActionResult ProductPage(int? sayfa, int? kategori_id, int? renk_id)
        {
            IPagedList<Urun> urun;
             if (kategori_id == null)
            {
                if (renk_id==null)
                {
                    urun = db.Urun.OrderBy(x => Guid.NewGuid()).ToPagedList<Urun>(sayfa ?? 1, 10);
                }
                else
                {
                    urun = db.Urun.Where(x=>x.renk_id==renk_id).OrderBy(x => Guid.NewGuid()).ToPagedList<Urun>(sayfa ?? 1, 10);
                }
            }
            else
            {
                if (renk_id == null)
                {
                    urun = db.Urun.Where(x => x.kategori_id == kategori_id).OrderBy(x => Guid.NewGuid()).ToPagedList<Urun>(sayfa ?? 1, 10);
                }
                else
                {
                    urun = db.Urun.Where(x => x.kategori_id == kategori_id && x.renk_id==renk_id).OrderBy(x => Guid.NewGuid()).ToPagedList<Urun>(sayfa ?? 1, 10);
                }
            }
            return View(urun);
        }
        public ActionResult Kategoriler()
        {
            var kategoriler = db.Kategori.OrderBy(x => x.ad);
            return View(kategoriler);
        }
        public ActionResult ParaFilter()
        {
            return View();
        }
        public ActionResult RenkFilter()
        {
            var renkler = db.Renk;
            return View(renkler);
        }
        public ActionResult AramaFilter()
        {
            return View();
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
                    return RedirectToAction("Index","Sepet");
                }
                else
                {
                    sepeteEkliMi.urun_adet += 1;
                    db.SaveChanges();
                    return RedirectToAction("Index","Sepet");
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