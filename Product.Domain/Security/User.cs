using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Product.Core.Contracts.Entities;

namespace Product.Domain.Security
{
    public class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

    }
}
