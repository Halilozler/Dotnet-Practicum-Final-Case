using System;
using Final.Base.Response;
using Final.Dto.Dtos;
using Final.Dto.Models;

namespace Final.Service.TokenOperations.Abstract
{
	public interface ITokenManagementService
	{
        Task<BaseResponse<TokenResponse>> GenerateTokensAsync(UserDto tokenRequest, DateTime now, string userAgent);
    }
}

