using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Base.Response;
using Final.Data.Model.DatabaseSql;
using Final.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_Case.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : CustomBaseController
    {
        private readonly IListsService _service;

        public ListsController(IListsService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lists dto)
        {
            var response = await _service.InsertAsync(dto);

            return CreateActionResultInstance(response);
        }
    }
}
