using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebKorpa.Models
{
    [Table("Kategorija")]
    public partial class Kategorija
    {
        public Kategorija()
        {
            Proizvodi = new HashSet<Proizvod>();
        }

        public int KategorijaId { get; set; }
        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        [InverseProperty("Kategorija")]
        public virtual ICollection<Proizvod> Proizvodi { get; set; }
    }
}