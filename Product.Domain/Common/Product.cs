using Product.Core.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Common
{
    public class Products : BaseEntity
    {
        [StringLength(300)]
        public string Name { get; set; }

        [Key, Column(Order = 0)]
        public DateTime ProduceDate { get; set; }

        [StringLength(50)]
        [Key, Column(Order = 1)]
        [Required, EmailAddress]
        public string ManufactureEmail { get; set; }


        [StringLength(11)]
        public string ManufacturePhone { get; set; }

        public bool IsAvailable { get; set; }

    }
}
