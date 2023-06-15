using Product.Common.DTOs.Common;
using Product.Common.DTOs.Security;
using Product.Core.Biz;
using Product.Core.DataAccess;
using Product.Core.Module;
using Product.Domain.Common;
using Product.Domain.Security;
using Product.Services.Contracts.Common;
using Product.Services.Contracts.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Product.Services.Modules.Security
{
    public class UserBiz : BaseBiz<User, UserDTO>, IUserBiz
    {
        public UserBiz(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        private List<Claim> getClaims(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));

            return claims;
        }

        public Tuple<UserDTO, List<Claim>> Login()
        {
            var user = new User()
            {
                Id = 1,
                Username = "test"
            };
            //var user = UnitOfWork.Repository<User>()
            //    .Get(x => (x.Username == loginDto.Username))
            //    .FirstOrDefault();

            //if (user == null)
            //    return null;

            var claims = getClaims(user);
            return new Tuple<UserDTO, List<Claim>>(user.ToDTO<UserDTO>(), claims);
        }
    }
}
