using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SolidTA.DAL.Entities
{


    [Table("Currency")]
    public partial class Currency
    {
        public Currency()
        {
            Rate = new List<Rate>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CurrencyID { get; set; }

        [Required]
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string NumCode { get; set; }

        [Required]
        [StringLength(50)]
        public string CharCode { get; set; }

        public  List<Rate> Rate { get; set; }
    }
}
