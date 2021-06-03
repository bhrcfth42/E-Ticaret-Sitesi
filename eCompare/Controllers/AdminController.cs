using eCompare.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eCompare.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        eCompareDatabase db = new eCompareDatabase();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SideBar()
        {
            return View();
        }
        public ActionResult Footer()
        {
            return View();
        }
        public ActionResult TopBar()
        {
            if (Session["kullanici_id"] != null)
            {
                int id = (int)Session["kullanici_id"];
                var kullanici = db.Kullanici.Where(x => x.id == id).FirstOrDefault();
                return View(kullanici);
            }
            else
            {
                return null;
            }
        }
        public ActionResult Icerik()
        {
            if (Session["kullanici_id"] != null)
            {
                int id = (int)Session["kullanici_id"];
                var kullanici = db.Kullanici.Where(x => x.id == id).FirstOrDefault();
                var urunler = db.Urun.Where(x => x.kullanici_id == kullanici.id);
                ViewBag.Kullanici = kullanici;
                return View(urunler);
            }
            return null;
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult UrunEkle()
        {
            return View();
        }
        public ActionResult UrunEkleIcerik()
        {
            ViewBag.kategori_id = new SelectList(db.Kategori, "id", "ad");
            ViewBag.beden_id = new SelectList(db.Beden, "id", "no");
            ViewBag.renk_id = new SelectList(db.Renk, "id", "adi");
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkleIcerik(Urun urun, HttpPostedFileBase video, IEnumerable<HttpPostedFileBase> resim)
        {
            int id = (int)Session["kullanici_id"];
            var kullanici = db.Kullanici.Where(x => x.id == id).FirstOrDefault();
            urun.kullanici_id = kullanici.id;
            urun.Resim = ResimKaydet(resim);
            if (video != null && video.ContentLength > 0)
                urun.Video = VideoKaydet(video);
            urun.tarih = DateTime.Now;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid)
            {
                db.Urun.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.kategori_id = new SelectList(db.Kategori, "id", "ad");
                ViewBag.beden_id = new SelectList(db.Beden, "id", "no");
                ViewBag.renk_id = new SelectList(db.Renk, "id", "adi");
                return View();
            }

        }
        public Resim ResimKaydet(IEnumerable<HttpPostedFileBase> files)
        {
            List<Resim> resimler = new List<Resim>();
            string oncekiresimyol = "";
            for (int i = files.Count() - 1; i >= 0; i--)
            {
                HttpPostedFileBase item = files.ElementAt(i);
                if (item.ContentLength > 0)
                {
                    string dosyaAdi = Path.GetFileNameWithoutExtension(item.FileName) + Guid.NewGuid() + Path.GetExtension(item.FileName);
                    dosyaAdi = dosyaAdi.Trim().Replace(" ", string.Empty);
                    FileStream fs = new FileStream(Server.MapPath("/Content/images/Photos/" + dosyaAdi), FileMode.Create);
                    byte[] buffer = new byte[item.ContentLength];
                    item.InputStream.Read(buffer, 0, item.ContentLength);
                    fs.Write(buffer, 0, item.ContentLength);
                    fs.Close();
                    Resim resim = new Resim();
                    resim.yol = "/Content/images/Photos/" + dosyaAdi;
                    if (oncekiresimyol != "")
                    {
                        resim.ust_id = db.Resim.FirstOrDefault(x => x.yol == oncekiresimyol).id;
                    }
                    db.Resim.Add(resim);
                    db.SaveChanges();
                    oncekiresimyol = resim.yol;
                    resimler.Add(resim);
                }
            }
            return resimler.Last();
        }
        public Video VideoKaydet(HttpPostedFileBase file)
        {
            string dosyaAdi = Path.GetFileNameWithoutExtension(file.FileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
            dosyaAdi = dosyaAdi.Trim().Replace(" ", string.Empty);
            FileStream fs = new FileStream(Server.MapPath("/Content/videos/" + dosyaAdi), FileMode.Create);
            byte[] buffer = new byte[file.ContentLength];
            file.InputStream.Read(buffer, 0, file.ContentLength);
            fs.Write(buffer, 0, file.ContentLength);
            fs.Close();
            Video video = new Video();
            video.yol = "/Content/videos/" + dosyaAdi;
            db.Video.Add(video);
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            db.SaveChanges();
            return video;
        }
        public ActionResult UrunDuzenle()
        {
            return View();
        }
        public ActionResult DuzenlemeIcerik(int id)
        {
            Urun seciliUrun = db.Urun.FirstOrDefault(x => x.id == id);
            ViewBag.kategori_id = new SelectList(db.Kategori, "id", "ad", seciliUrun.kategori_id);
            ViewBag.beden_id = new SelectList(db.Beden, "id", "no", seciliUrun.beden_id);
            ViewBag.renk_id = new SelectList(db.Renk, "id", "adi", seciliUrun.renk_id);
            return View(seciliUrun);
        }
        public ActionResult UrunSil(int id)
        {
            db.Urun.Remove(db.Urun.FirstOrDefault(x => x.id == id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DuzenlemeIcerik(Urun urun, HttpPostedFileBase video, IEnumerable<HttpPostedFileBase> resim)
        {
            Urun secili = db.Urun.FirstOrDefault(x => x.id == urun.id);
            secili.adi = urun.adi;
            secili.detay = urun.detay;
            secili.fiyat = urun.fiyat;
            secili.stok = urun.stok;
            secili.kategori_id = urun.kategori_id;
            secili.beden_id = urun.beden_id;
            secili.renk_id = urun.renk_id;
            if (resim.First() != null)
            {
                secili.resim_id = ResimKaydet(resim).id;
            }
            if (video != null && video.ContentLength > 0)
            {
                secili.video_id = VideoKaydet(video).id;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BedenEkle()
        {
            return View();
        }
        public ActionResult BedenEkleIcerik()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BedenEkle(Beden yeni)
        {
            db.Beden.Add(yeni);
            db.SaveChanges();
            return RedirectToAction("BedenList");
        }
        public ActionResult BedenList()
        {
            return View();
        }
        public ActionResult BedenListIcerik()
        {
            if (Session["kullanici_id"] != null)
            {
                int id = (int)Session["kullanici_id"];
                var kullanici = db.Kullanici.Where(x => x.id == id).FirstOrDefault();
                var bedenler = db.Beden;
                return View(bedenler);
            }
            return null;
        }
        public ActionResult BedenDuzenle()
        {
            return View();
        }
        public ActionResult BedenDuzenleIcerik(int id)
        {
            Beden seciliBeden = db.Beden.FirstOrDefault(x => x.id == id);
            return View(seciliBeden);
        }
        [HttpPost]
        public ActionResult BedenDuzenle(Beden yeni)
        {
            Beden secili = db.Beden.FirstOrDefault(x => x.id == yeni.id);
            secili.no = yeni.no;
            db.SaveChanges();
            return RedirectToAction("BedenList");
        }
        public ActionResult BedenSil(int id)
        {
            db.Beden.Remove(db.Beden.FirstOrDefault(x => x.id == id));
            db.SaveChanges();
            return RedirectToAction("BedenList");
        }
        public ActionResult KategoriEkle()
        {
            return View();
        }
        public ActionResult KategoriEkleIcerik()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori yeni, HttpPostedFileBase video, HttpPostedFileBase resim)
        {
            yeni.resim_yol = ResimKaydetTek(resim).yol;
            if (video != null && video.ContentLength > 0)
                yeni.video_yol = VideoKaydet(video).yol;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            return RedirectToAction("KategoriList");
        }
        public ActionResult KategoriList()
        {
            return View();
        }
        public ActionResult KategoriListIcerik()
        {
            if (Session["kullanici_id"] != null)
            {
                int id = (int)Session["kullanici_id"];
                var kullanici = db.Kullanici.Where(x => x.id == id).FirstOrDefault();
                var kategoriler = db.Kategori;
                return View(kategoriler);
            }
            return null;
        }
        public ActionResult KategoriDuzenle()
        {
            return View();
        }
        public ActionResult KategoriDuzenleIcerik(int id)
        {
            Kategori seciliKategori = db.Kategori.FirstOrDefault(x => x.id == id);
            return View(seciliKategori);
        }
        [HttpPost]
        public ActionResult KategoriDuzenle(Kategori yeni, HttpPostedFileBase video, HttpPostedFileBase resim)
        {
            Kategori secili = db.Kategori.FirstOrDefault(x => x.id == yeni.id);
            if (resim != null && resim.ContentLength > 0)
                secili.resim_yol = ResimKaydetTek(resim).yol;
            if (video != null && video.ContentLength > 0)
                secili.video_yol = VideoKaydet(video).yol;
            secili.ad = yeni.ad;
            db.SaveChanges();
            return RedirectToAction("KategoriList");
        }
        public ActionResult KategoriSil(int id)
        {
            db.Kategori.Remove(db.Kategori.FirstOrDefault(x => x.id == id));
            db.SaveChanges();
            return RedirectToAction("KategoriList");
        }
        public Resim ResimKaydetTek(HttpPostedFileBase files)
        {
            string dosyaAdi = Path.GetFileNameWithoutExtension(files.FileName) + Guid.NewGuid() + Path.GetExtension(files.FileName);
            dosyaAdi = dosyaAdi.Trim().Replace(" ", string.Empty);
            FileStream fs = new FileStream(Server.MapPath("/Content/images/Photos/" + dosyaAdi), FileMode.Create);
            byte[] buffer = new byte[files.ContentLength];
            files.InputStream.Read(buffer, 0, files.ContentLength);
            fs.Write(buffer, 0, files.ContentLength);
            fs.Close();
            Resim resim = new Resim();
            resim.yol = "/Content/images/Photos/" + dosyaAdi;
            db.Resim.Add(resim);
            db.SaveChanges();
            return resim;
        }

        public ActionResult RenkEkle()
        {
            return View();
        }
        public ActionResult RenkEkleIcerik()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RenkEkle(Renk yeni)
        {
            db.Renk.Add(yeni);
            db.SaveChanges();
            return RedirectToAction("RenkList");
        }
        public ActionResult RenkList()
        {
            return View();
        }
        public ActionResult RenkListIcerik()
        {
            if (Session["kullanici_id"] != null)
            {
                int id = (int)Session["kullanici_id"];
                var kullanici = db.Kullanici.Where(x => x.id == id).FirstOrDefault();
                var renkler = db.Renk;
                return View(renkler);
            }
            return null;
        }
        public ActionResult RenkDuzenle()
        {
            return View();
        }
        public ActionResult RenkDuzenleIcerik(int id)
        {
            Renk seciliRenk = db.Renk.FirstOrDefault(x => x.id == id);
            return View(seciliRenk);
        }
        [HttpPost]
        public ActionResult RenkDuzenle(Renk yeni)
        {
            Renk secili = db.Renk.FirstOrDefault(x => x.id == yeni.id);
            secili.adi = yeni.adi;
            secili.kod = yeni.kod;
            db.SaveChanges();
            return RedirectToAction("RenkList");
        }
        public ActionResult RenkSil(int id)
        {
            db.Renk.Remove(db.Renk.FirstOrDefault(x => x.id == id));
            db.SaveChanges();
            return RedirectToAction("RenkList");
        }
        public ActionResult Siparis()
        {
            return View();
        }
        public ActionResult SiparisIcerik()
        {
            if (Session["kullanici_id"] != null)
            {
                int id = (int)Session["kullanici_id"];
                var kullanici = db.Kullanici.Where(x => x.id == id).FirstOrDefault();
                var siparisler = kullanici.Siparis;
                return View(siparisler);
            }
            return null;
        }
    }
}