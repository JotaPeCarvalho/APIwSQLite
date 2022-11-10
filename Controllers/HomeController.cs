using Microsoft.AspNetCore.Mvc;
using MinhaApi.Data;
using MinhaApi.Models;

namespace MinhaApi.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase 
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices]AppDbContext context) => 
        Ok(context.TodoModels.ToList());
        

        [HttpGet("/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices]AppDbContext context) 
        {
           var todos = context.TodoModels.FirstOrDefault(x => x.Id == id);

           if(todos == null)
            return NotFound();

            return  Ok(todos);
        }

        [HttpPost("/")]
        public IActionResult Post([FromBody] TodoModel model, [FromServices]AppDbContext context) 
        {
           context.TodoModels.Add(model);
           context.SaveChanges();
           return Created($"/{model.Id}", model);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] TodoModel todo, [FromServices]AppDbContext context) 
        {
            TodoModel modelo = context.TodoModels.FirstOrDefault(x => x.Id == id);
            if(modelo == null)
                return NotFound();

                modelo.Title = todo.Title;
                modelo.Done = todo.Done;
            context.TodoModels.Update(modelo);
            context.SaveChanges();
            return Ok(modelo);
            
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices]AppDbContext context) 
        {
            TodoModel model = context.TodoModels.FirstOrDefault(x => x.Id == id);
            if(model == null)
                return NotFound();
           context.TodoModels.Remove(model);
           context.SaveChanges();
           return Ok(model);
        }
    }
}