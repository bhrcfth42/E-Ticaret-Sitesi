using eCompare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCompare.Controllers
{
    public class SepetController : Controller
    {
        eCompareDatabase db = new eCompareDatabase();
        // GET: Sepet
        public ActionResult Index()
        {
            if(Session["kullanici_id"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
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
        public ActionResult TitlePage()
        {
            return View();
        }
        public ActionResult SepetIcerik()
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
        public ActionResult SepettenUrunSil(int id)
        {
            db.Sepet.Remove(db.Sepet.FirstOrDefault(x => x.id == id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}