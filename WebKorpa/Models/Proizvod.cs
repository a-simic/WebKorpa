using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebKorpa.Models
{
    [Table("Proizvod")]
    public partial class Proizvod
    {
        public Proizvod()
        {
            Stavke = new HashSet<Stavka>();
        }

        public int ProizvodId { get; set; }
        public int KategorijaId { get; set; }
        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Cena { get; set; }
        [StringLength(100)]
        public string Opis { get; set; }

        [ForeignKey("KategorijaId")]
        [InverseProperty("Proizvodi")]
        public virtual Kategorija Kategorija { get; set; }
        [InverseProperty("Proizvod")]
        public virtual ICollection<Stavka> Stavke { get; set; }
    }
}