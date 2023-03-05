using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Final.Base.Response;
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
        private readonly IListsService<CreateListDto> _service;

        public ListsController(IListsService<CreateListDto> service)
        {
            _service = service;
        }

        
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Create(CreateListDto dto)
        {
            //Ben burada NameIdentifier kısmına Id koymuştum onu almak için yazdık.
            var userId = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier).Value;
            //yada
            userId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
            return Ok();
        }
        /*
        [HttpGet]
        public async Task<IActionResult> GetLists()
        {
            return CreateActionResultInstance(null);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateList([FromBody] UpdateListDto dto)
        {
            //userId
            return CreateActionResultInstance(null);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteList(int ListId)
        {

            return CreateActionResultInstance(null);
        }
        */
    }
}
