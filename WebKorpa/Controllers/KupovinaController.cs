using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebKorpa.Servisi;
using Microsoft.AspNetCore.Identity;
using WebKorpa.Data;
using Microsoft.AspNetCore.Authorization;
using WebKorpa.Models;


namespace WebKorpa.Controllers
{
    public class KupovinaController : Controller
    {
        private readonly ProdavnicaContext db;
        private readonly UserManager<ApplicationUser> um;

        private KorpaServis kServis;


        public KupovinaController(ProdavnicaContext _db, UserManager<ApplicationUser> _um, KorpaServis _kServis)
        {
            db = _db;
            um = _um;
            kServis = _kServis;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            Korpa korpa = kServis.CitajKorpu();
            if (korpa.Stavke.Count() == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ApplicationUser user = await um.GetUserAsync(User);
            string id = user.Id;
            Porudzbina p1 = new Porudzbina
            {
                KupacId = id,
                DatumKupovine = DateTime.Now
            };
            try
            {
                db.Porudzbine.Add(p1);
                db.SaveChanges();
                int pId = p1.PorudzbinaId;

                foreach (StavkaKorpe st in korpa.Stavke)
                {
                    Stavka st1 = new Stavka
                    {
                        PorudzbinaId = pId,
                        ProizvodId = st.Proizvod.ProizvodId,
                        Kolicina = st.Kolicina
                    };

                    db.Stavke.Add(st1);
                    db.SaveChanges();
                }


                kServis.ObrisiKorpu();

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");

            }

        }
    }
}