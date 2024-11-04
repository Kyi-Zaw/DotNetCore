using KZDotNetCore.Domain.Features.Snake;
using KZDotNetCore.Domain.Features.Todo;
using KZDotNetCore.Domain.Models.Snake;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIDDotNetTraining.Domain.Models;

namespace KZDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnakeController : ControllerBase
    {

        private readonly ISnakeService _iSnakeService;

        public SnakeController(ISnakeService snakeService)
        {
            _iSnakeService = snakeService;
        }

        [HttpPost]
        public IActionResult Create(SnakeRequestModel requestModel)
        {
            var model = _iSnakeService.Create(requestModel);
            if (model.Response.IsError)
            {
                return BadRequest(model);
            }

            return Ok(model);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = _iSnakeService.Get();
            if (model.Response.IsError)
            {
                return BadRequest(model);
            }
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var model = _iSnakeService.GetByID(id);
            if (model.Response.IsError)
            {
                return BadRequest(model);
            }
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, SnakeRequestModel requestModel)
        {
            var model = _iSnakeService.Update(id, requestModel);
            if (model.Response.IsError)
            {
                return BadRequest(model);
            }

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _iSnakeService.Delete(id);
            if (model.Response.IsError)
            {
                return BadRequest(model);
            }

            return Ok(model);
        }
    }
}
