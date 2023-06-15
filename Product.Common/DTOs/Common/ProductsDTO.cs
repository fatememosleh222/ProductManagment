using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Product.Core.Contracts.Entities;
using Product.Core.Contracts.DTOs;

namespace Product.Common.DTOs.Common
{
    public class ProductsDTO : BaseEntityDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime ProduceDate { get; set; }

        [Required, EmailAddress]
        public string ManufactureEmail { get; set; }

        public string ManufacturePhone { get; set; }

        public bool IsAvailable { get; set; }

    }
}
