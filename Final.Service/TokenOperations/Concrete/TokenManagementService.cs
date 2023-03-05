using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Final.Base.Jwt;
using Final.Base.Response;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.UnitOfWork;
using Final.Dto.Dtos;
using Final.Dto.Models;
using Final.Service.TokenOperations.Abstract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Final.Service.TokenOperations.Concrete
{
    public class TokenManagementService : ITokenManagementService
    {
        private readonly IUserRepository genericRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtConfig _jwtConfig;
        private readonly byte[] _secret;

        public TokenManagementService(IUserRepository genericRepository, IMapper mapper, IUnitOfWork unitOfWork, IOptionsMonitor<JwtConfig> jwtConfig)
        {
            this.genericRepository = genericRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jwtConfig = jwtConfig.CurrentValue;
            _secret = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
        }

        public async Task<BaseResponse<TokenResponse>> GenerateTokensAsync(UserDto tokenRequest, DateTime now, string userAgent)
        {
            var account = genericRepository.Where(x => x.Name == tokenRequest.Name).FirstOrDefault();
            if (account is null)
            {
                return BaseResponse<TokenResponse>.Fail("InvalidUserInformation", 401);
            }

            if (account.Password != tokenRequest.Password)
            {
                return BaseResponse<TokenResponse>.Fail("InvalidUserInformation", 401);
            }

            var token = GenerateAccessToken(account, now);

            //account.LastActivity = DateTime.Now;
            //_unitOfWork.AccountRepository.Update(account);
            //await _unitOfWork.CompleteAsync();

            TokenResponse response = new TokenResponse
            {
                AccessToken = token,
                ExpireTime = now.AddMinutes(_jwtConfig.AccessTokenExpiration),
                Role = account.RoleId.ToString(),
                Id = account.Id.ToString()
            };

            return BaseResponse<TokenResponse>.Success(response,200);
        }

        private string GenerateAccessToken(User user, DateTime now)
        {
            // Get claim value
            Claim[] claims = GetClaim(user);

            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

            //create jwt token
            var jwtToken = new JwtSecurityToken(
                _jwtConfig.Issuer,
                shouldAddAudienceClaim ? _jwtConfig.Audience : string.Empty,
                claims,
                expires: now.AddMinutes(_jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));

            var userToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return userToken;
        }

        private static Claim[] GetClaim(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim("AccountId", user.Id.ToString()),
                //new Claim("LastActivity", account.LastActivity.ToLongTimeString())
            };

            return claims;
        }
    }
}

