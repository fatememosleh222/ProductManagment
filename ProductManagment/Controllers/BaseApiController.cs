using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Common.DTOs.Common;

namespace ProductManagment.Controllers
{
    [Authorize]
    internal class BaseApiController : ControllerBase
    {
        public OkObjectResult Okk()
        {
            return Ok(new BaseResponse(true));
        }
        public OkObjectResult Okk(bool succeed, string errorMessage)
        {
            return Ok(new BaseResponse(succeed, errorMessage));
        }
        public OkObjectResult Okk(bool succeed, string errorMessage, object data)
        {
            return Ok(new BaseResponse(succeed, errorMessage) { Data = data });
        }
    }
}
