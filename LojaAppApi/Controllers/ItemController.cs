using LojaAppApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LojaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var entities = await _itemService.GetAll();
                if (entities == null)
                    return NotFound();

                return Ok(entities);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
