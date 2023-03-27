using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using StokMvc.Models.Entity;

namespace StokMvc.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrunEkle()
        {
            if (!ModelState.IsValid)
            {
                return View("YeniUrunEkle");
            }
            List<SelectListItem>degerler=(from i in db.TBLKATEGORILER.ToList()
                select new SelectListItem
                {
                    Text= i.KATEGORIAD,
                    Value= i.KATEGORIID.ToString()
              
                }).ToList();

            ViewBag.dgr = degerler;
            return View();
        
        
        }



        [HttpPost]
        public ActionResult YeniUrunEkle(TBLURUNLER u1)
        {
            var ktg=db.TBLKATEGORILER.Where(m=>m.KATEGORIID==u1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            u1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(u1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urunler = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urunler);
            db.SaveChanges
                ();

            return RedirectToAction("Index");

        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.KATEGORIAD,
                                                Value = i.KATEGORIID.ToString()

                                            }).ToList();

            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urun = db.TBLURUNLER.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;
            urun.STOK = p.STOK;
            urun.FIYAT = p.FIYAT;
            //urun.URUNKATEGORI = p.URUNKATEGORI;

            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}