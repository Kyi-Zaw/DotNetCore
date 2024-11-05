using KZDotNetCore.Domain.Features;
using KZDotNetCore.Domain.Features.Todo;
using KZDotNetCore.Domain.Models.Todo;
using Microsoft.AspNetCore.Mvc;

namespace KZDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ITodoServices _todoService;

        public ToDoController(ITodoServices todoService)
        {
            _todoService = todoService;
        }

        [HttpPost]
        public IActionResult Create(TodoRequestModel requestModel)
        {
            var model = _todoService.Create(requestModel);
            if (model.Response.IsError)
            {
                return BadRequest(model);
            }

            return Ok(model);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = _todoService.Get();
            if (model.Response.IsError)
            {
                //return StatusCode(500, model);
                return BadRequest(model);
            }
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(string id)
        {
            var model = _todoService.GetByID(id);
            if (model.Response.IsError)
            {
                //return StatusCode(500, model);
                return BadRequest(model);
            }
            return Ok(model);
        }
    }
}
