using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Contracts.DTOs
{
    public class BaseEntityDTO
    {
        public int? CreatorId { get; set; }
        public bool IsDelete { get; set; }

    }
}
