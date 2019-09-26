using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebKorpa.Models
{
    [Table("Stavka")]
    public partial class Stavka
    {
        public int StavkaId { get; set; }
        public int PorudzbinaId { get; set; }
        public int ProizvodId { get; set; }
        public int Kolicina { get; set; }

        [ForeignKey("PorudzbinaId")]
        [InverseProperty("Stavke")]
        public virtual Porudzbina Porudzbina { get; set; }
        [ForeignKey("ProizvodId")]
        [InverseProperty("Stavke")]
        public virtual Proizvod Proizvod { get; set; }
    }
}