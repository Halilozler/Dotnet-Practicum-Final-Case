using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Final.Base.Services
{
	public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _contextAccessor;

        public IdentityService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int GetUserId => Int32.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}

