using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Common.DTOs.Common;

namespace ProductManagment.Controllers
{
   // [Authorize]
    public class BaseApiController : ControllerBase
    {
        [NonAction]
        public OkObjectResult Okk()
        {
            return Ok(new BaseResponse(true));
        }

        [NonAction]
        public OkObjectResult Okk(bool succeed, string errorMessage)
        {
            return Ok(new BaseResponse(succeed, errorMessage));
        }

        [NonAction]
        public OkObjectResult Okk(bool succeed, string errorMessage, object data)
        {
            return Ok(new BaseResponse(succeed, errorMessage) { Data = data });
        }
    }
}
