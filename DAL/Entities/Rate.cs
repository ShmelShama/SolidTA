using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SolidTA.DAL.Entities
{


    [Table("Rate")]
    public partial class Rate
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RateID { get; set; }

       
        public int CurrencyID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public short Nominal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Value { get; set; }

        [ForeignKey(nameof(CurrencyID))]
        public Currency Currency { get; set; }
    }
}
