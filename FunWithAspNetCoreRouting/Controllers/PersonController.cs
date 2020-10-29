using System;
using System.Threading.Tasks;
using FunWithAspNetCoreRouting.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FunWithAspNetCoreRouting.Api.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IPersonService service;

        public PersonController(IPersonService service)
        {
            this.service = service;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAsync()
        //{
        //    return Ok(await service.GetAsync());
        //}

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]string firstName)
        {
            return Ok(await service.GetAsync(firstName));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(await service.GetAsync(id));
        }
    }
}