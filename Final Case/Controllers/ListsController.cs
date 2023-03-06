using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Final.Base.Response;
using Final.Base.Services;
using Final.Data.Model.DatabaseSql;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_Case.Controllers
{
    //We automatically retrieve the User ID from the token.
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : CustomBaseController
    {
        private readonly IListsService _service;
        private readonly IIdentityService _identityService;

        public ListsController(IListsService service, IIdentityService identityService)
        {
            _service = service;
            _identityService = identityService;
        }

        //1 is Normal user
        //2 is Admin

        //List tamamlandı.
        //Tamamlanan Liste mongo db getir.

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Create(CreateListDto dto)
        {
            int userId = _identityService.GetUserId;
            dto.UserId = userId;
            var response = await _service.InsertAsync(dto);
            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetLists()
        {
            int userId = _identityService.GetUserId;
            var response = await _service.GetByUserIdAsync(userId);
            return CreateActionResultInstance(response);
        }

        [HttpGet("search")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> SearchLists([FromQuery] string? name, string? catName)
        {
            int userId = _identityService.GetUserId;
            var response = await _service.Search(userId, name, catName);
            return CreateActionResultInstance(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateList(int id, [FromBody] UpdateListDto dto)
        {
            int userId = _identityService.GetUserId;
            var response = await _service.UpdateAsync(id, dto, userId);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{ListId}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteList(int ListId)
        {
            int userId = _identityService.GetUserId;
            var response = await _service.RemoveAsync(ListId, userId);
            return CreateActionResultInstance(response);
        }

        [HttpPut("CompleteList/{listId}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CompleteList(int listId)
        {
            int userId = _identityService.GetUserId;
            var response = await _service.CompleteList(listId, userId);
            return CreateActionResultInstance(response);
        }

        [HttpGet("CompleteList")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetCompleteList()
        {
            int userId = _identityService.GetUserId;
            var response = await _service.GetCompleteList(userId);
            return CreateActionResultInstance(response);
        }
    }
}
