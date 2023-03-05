using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Base.Response;
using Final.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_Case.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : CustomBaseController
    {
        private readonly IListsService _service;

        public AdminController(IListsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> CompleteList()
        {
            var response = await _service.AdminGetList();
            return CreateActionResultInstance(response);
        }
    }
}
