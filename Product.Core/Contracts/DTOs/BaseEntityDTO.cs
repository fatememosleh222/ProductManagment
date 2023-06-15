using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Core.Contracts.DTOs;

namespace Product.Core.Contracts.DTOs
{
    public abstract class BaseEntityDTO //: IEntityDTO
    {
        public BaseEntityDTO()
        {
            DateInserted = DateTime.Now;
        }


        public Guid? UId { get; set; }
        public int CreatorId { get; set; }
        public DateTime DateInserted { get; set; }
        public DateTime? DateModified { get; set; }
        public int? UpdaterId { get; set; }
        public bool IsDelete { get; set; }

    }
}
