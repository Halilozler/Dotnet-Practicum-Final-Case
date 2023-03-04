using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Final.Base.Response;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_Case.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService<CreateUserDto> _createUserService;
        private readonly IUserService<UserDto> _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService<CreateUserDto> createUserService, IUserService<UserDto> userService, IRoleService roleService)
        {
            _createUserService = createUserService;
            _userService = userService;
            _roleService = roleService;
        }

        //Create User
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var role = await _roleService.GetByIdAsync(dto.RoleId);

            var response = await _createUserService.InsertAsync(dto);

            return CreateActionResultInstance(response);
        }

        //User Query
        [HttpPost]
        public async Task<IActionResult> Query([FromBody] UserDto dto)
        {
            return null;
        }
    }
}
