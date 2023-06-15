using Product.Common.DTOs.Common;
using Product.Core.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Services.Contracts.Common
{
    public interface  IProductBiz: IBiz<ProductsDTO>
    {
        bool Insert(ProductsDTO data, out string errorMessage);
        bool Update(ProductsDTO data, out string errorMessage);
        List<ProductsDTO> GetList();

    }
}
