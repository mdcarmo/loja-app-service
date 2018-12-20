using LojaAppApi.Model;
using LojaAppApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LojaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [AllowAnonymous]
        [HttpGet]

        public async Task<ActionResult> GetAll()
        {
            try
            {
                var entities = await _customerService.GetAll();
                if (entities == null)
                    return NotFound();

                return Ok(entities);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("getbyid")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            try
            {
                var entitie = await _customerService.GetById(id);

                if (entitie == null)
                    return NotFound();

                return Ok(entitie);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody]Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entityId = await _customerService.Add(customer);
                    if (entityId > 0)
                        return Ok(entityId);
                    else
                        return NotFound();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}
