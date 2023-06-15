using Product.Common.DTOs.Common;
using Product.Core.Biz;
using Product.Core.DataAccess;
using Product.Core.Module;
using Product.Domain.Common;
using Product.Services.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Services.Modules.Common
{
    public class ProductBiz : BaseBiz<Products, ProductsDTO>, IProductBiz
    {
        public ProductBiz(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        private bool CheckValidation(ProductsDTO data, out string errorMessage)
        {
            errorMessage = "";
            var isDuplicate = UnitOfWork.Repository<Products>().Get(e => e.ManufactureEmail == data.ManufactureEmail && e.ProduceDate == data.ProduceDate);
            if (data.Id > 0)
            {
                isDuplicate = isDuplicate.Where(e => e.Id != data.Id);
            }
            if (isDuplicate.Any())
            {
                errorMessage = "اطلاعات محصول تکراری می باشد";
                return false;
            }
            return true;
        }
        public bool Insert(ProductsDTO data, out string errorMessage)
        {
            errorMessage = "";
            var succeed = CheckValidation(data, out errorMessage);
            if (!succeed)
            {
                return false;
            }
            Insert(data, out errorMessage);
            return true;
        }
        public bool Update(ProductsDTO data, out string errorMessage)
        {
            errorMessage = "";
            var succeed = CheckValidation(data, out errorMessage);
            if (!succeed)
            {
                return false;
            }
            Update(data, out errorMessage);
            return true;
        }

        public List<ProductsDTO> GetList()
        {
            return UnitOfWork.Repository<Products>().Get().ToDTOList<ProductsDTO>();
        }
    }
}
