using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace financeiro.Models
{
    public class Relatoriodespesa
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Despesa")]
        public string ItemNome { get; set; }

        [Required]
        [DataType(DataType.Currency)] //currency é do tipo Moeda
        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataDespesa { get; set; } = DateTime.Now;

        [Required]
        [StringLength(200)]
        public string Categoria { get; set; }
    }
}
