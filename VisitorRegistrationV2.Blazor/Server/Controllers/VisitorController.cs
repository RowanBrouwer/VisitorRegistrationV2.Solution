using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Data.Services.Visitors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VisitorRegistrationV2.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitors context;
        public VisitorController(IVisitors context)
        {
            this.context = context;
        }


        // GET: api/<VisitorController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visitor>>> Get()
        {
            var result = await context.GetListOfVisitors();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/<VisitorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VisitorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VisitorController>/5
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] Visitor visitor)
        {
            return Task.FromResult(context.UpdateVisitor(visitor));
        }

        // DELETE api/<VisitorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
