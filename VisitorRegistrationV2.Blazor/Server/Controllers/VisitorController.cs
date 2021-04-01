using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Data.Services.Hubs;
using VisitorRegistrationV2.Data.Services.Visitors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VisitorRegistrationV2.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitors context;
        private readonly IHubContext<VisitorHub> hubContext;
        public VisitorController(IVisitors context, IHubContext<VisitorHub> hubContext)
        {
            this.context = context;
            this.hubContext = hubContext;
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
        public async Task<ActionResult> Get(int id)
        {
            var result = await context.GetVisitorById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/<VisitorController>
        [HttpPost]
        public async Task<ActionResult<Visitor>> Post([FromBody] Visitor newVisitor)
        {
            Visitor result = newVisitor;
            if (ModelState.IsValid)
            {
                result = await context.AddVisitor(newVisitor);

                if (result == null)
                {
                    return BadRequest();
                }
            }

            return Ok(result);
        }

        // PUT api/<VisitorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]Visitor visitor)
        {
            var result = await Task.FromResult(context.UpdateVisitor(visitor));

            if (result == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        // DELETE api/<VisitorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await context.DeleteVisitor(id);

            return Ok();
        }
    }
}
