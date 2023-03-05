using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Base.Response;
using Final.Base.Services;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_Case.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : CustomBaseController
    {
        private IListItemService _service;
        private readonly IIdentityService _identityService;

        public ItemController(IListItemService listItemService, IIdentityService identityService) 
        {
            _identityService = identityService;
            _service = listItemService;
        }

        //Ekleme
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Create([FromBody]CreateListItemDto dto)
        {
            int userId = _identityService.GetUserId;
            var response = await _service.InsertAsync(dto);
            return CreateActionResultInstance(response);
        }

        //yaptı yı true çekme
        [HttpPut("{itemId}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> ReceipCheckTrue(int itemId)
        {
            int userId = _identityService.GetUserId;
            var response = await _service.UpdateReceipt(itemId, userId);
            return CreateActionResultInstance(response);
        }
    }
}
