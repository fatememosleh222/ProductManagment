using Product.Common.DTOs.Common;
using Product.Common.DTOs.Security;
using Product.Core.Biz;
using Product.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Product.Services.Contracts.Security
{
    public interface IUserBiz : IBiz<UserDTO>
    {
        Tuple<UserDTO, List<Claim>> Login();
    }
}
