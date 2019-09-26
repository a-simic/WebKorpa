using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebKorpa.Models
{
    public class Korpa
    {
        private List<StavkaKorpe> listaStavki = new List<StavkaKorpe>();

        public void DodajStavku(Proizvod p, int kolicina)
        {
            StavkaKorpe st1 = listaStavki.SingleOrDefault(st => st.Proizvod.ProizvodId == p.ProizvodId);

            if (st1 == null)
            {
                st1 = new StavkaKorpe
                {
                    Proizvod = p,
                    Kolicina = kolicina
                };

                listaStavki.Add(st1);
            }
        }

        public void PromeniStavku(Proizvod p, int kolicina)
        {
            StavkaKorpe st1 = listaStavki.SingleOrDefault(st => st.Proizvod.ProizvodId == p.ProizvodId);

            if (st1 != null)
            {
                st1.Kolicina = kolicina;
            }
        }

        public void ObrisiStavku(int id)
        {
            StavkaKorpe st1 = listaStavki.SingleOrDefault(st => st.Proizvod.ProizvodId == id);
            if (st1 != null)
            {
                listaStavki.Remove(st1);
            }

        }

        public decimal VrednostKorpe()
        {
            return listaStavki.Sum(st => st.Kolicina * st.Proizvod.Cena);
        }

        public void ObrisiKorpu()
        {
            listaStavki.Clear();
        }
        public List<StavkaKorpe> Stavke => listaStavki;
    }

}
