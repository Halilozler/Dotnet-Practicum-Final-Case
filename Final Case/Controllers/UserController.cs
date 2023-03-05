using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Final.Base.Response;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;
using Final.Service.TokenOperations.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Final_Case.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService<CreateUserDto> _createUserService;
        private readonly IUserService<UserDto> _userService;
        private readonly IRoleService _roleService;
        private readonly ITokenManagementService _tokenService;

        public UserController(IUserService<CreateUserDto> createUserService, IUserService<UserDto> userService, IRoleService roleService, ITokenManagementService tokenService)
        {
            _createUserService = createUserService;
            _userService = userService;
            _roleService = roleService;
            _tokenService = tokenService;
        }

        //Create User
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var role = await _roleService.GetByIdAsync(dto.RoleId);
            if(role.IsSuccessful == false)
            {
                return CreateActionResultInstance(role);
            }
            var response = await _createUserService.InsertAsync(dto);

            return CreateActionResultInstance(response);
        }

        //User var mı geriye token döndürecek.
        //name ve password alıp kontrol edicek.
        [HttpPost]
        public async Task<IActionResult> Query([FromBody] UserDto dto)
        {
            var userAgent = Request.Headers["User-Agent"].ToString();
            var result = await _tokenService.GenerateTokensAsync(dto, DateTime.UtcNow, userAgent);

            if (result.IsSuccessful)
            {
                Log.Information($"User: {result.Data.Id}, Role: {result.Data.Role} is logged in system");
            }
            return CreateActionResultInstance(result);
        }
    }
}
