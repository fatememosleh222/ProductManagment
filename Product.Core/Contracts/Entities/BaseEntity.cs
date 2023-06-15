using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Core.Contracts.Entities;

namespace Product.Core.Contracts.Entities
{
    public abstract class BaseEntity
    {
        public int? CreatorId { get; set; }
        public bool IsDelete { get; set; }
    }

}
