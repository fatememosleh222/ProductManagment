using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Core.Contracts.Entities;

namespace Product.Core.Contracts.DTOs
{
	public abstract class  StrongEntityDTO : BaseEntityDTO
    {
        public int Id { get; set; }
	}
}
