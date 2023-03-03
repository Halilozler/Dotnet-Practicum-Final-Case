using System;
using Microsoft.AspNetCore.Mvc;

namespace Final.Base.Response
{
	public class CustomBaseController : ControllerBase
	{
        public IActionResult CreateActionResultInstance<T>(BaseResponse<T> response)
        {
            //normalde bizim işte Ok(), NotFound(), ... gibi döneriz ama ObjectResult oluşturarak bundan kurtulmuş olduk direk olarak statusCodu döndürürüz.
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}

