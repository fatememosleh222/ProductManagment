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
        [Key]
        public int Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }


        public DateTime ProduceDate { get; set; }

        [StringLength(50)]
        [Required]
        public string ManufactureEmail { get; set; }


        [StringLength(11)]
        public string ManufacturePhone { get; set; }

        public bool IsAvailable { get; set; }

    }
}
