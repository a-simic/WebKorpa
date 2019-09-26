using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebKorpa.Models;
using WebKorpa.Ekstenzije;


namespace WebKorpa.Servisi
{
    public class KorpaServis
    {

        private readonly IHttpContextAccessor accessor;

        public KorpaServis(IHttpContextAccessor _accessor)
        {
            accessor = _accessor;
        }

        public void CuvajKorpu(Korpa k)
        {
            ISession sesija = accessor.HttpContext.Session;
            sesija.SerijalizujKorpu(k);
        }

        public Korpa CitajKorpu()
        {
            Korpa k = null;
            ISession sesija = accessor.HttpContext.Session;
            if (sesija.DeserijalizujKorpu() != null)
            {
                k = sesija.DeserijalizujKorpu();
            }
            else
            {
                k = new Korpa();
            }

            return k;
        }

        public void ObrisiKorpu()
        {
            ISession sesija = accessor.HttpContext.Session;
            sesija.Clear();
        }

    }
}
