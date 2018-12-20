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
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService iOrderService)
        {
            _orderService = iOrderService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var entities = await _orderService.GetAll();
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
                var entitie = await _orderService.GetById(id);

                if (entitie == null)
                    return NotFound();

                return Ok(new { entitie.Item1, entitie.Item2 });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody]Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entityId = await _orderService.Add(order);
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

        [AllowAnonymous]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            int result = 0;

            try
            {
                result = await _orderService.Delete(id);
                if (result == 0)
                    return NotFound();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}
    }
}
