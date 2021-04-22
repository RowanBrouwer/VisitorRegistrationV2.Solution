using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;
using VisitorRegistrationV2.Data.Services.Visitors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VisitorRegistrationV2.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitors context;
        private ILogger<VisitorController> logger;
        public VisitorController(IVisitors context, ILogger<VisitorController> logger)
        {
            this.logger = logger;
            this.context = context;
        }


        // GET: api/<VisitorController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitorDTO>>> Get()
        {
            logger.LogInformation($"GET VisitorList API call at {DateTime.Now.ToShortTimeString()}");

            var queryResult = await context.GetListOfVisitors();

            if (queryResult == null)
            {
                logger.LogWarning($"NOTFOUND VisitorList at {DateTime.Now.ToShortTimeString()}");
                return NotFound();
            }

            List<VisitorDTO> Result = new List<VisitorDTO>();

            foreach (var visitor in queryResult)
            {
                var visitorDTO = await visitor.ConvertToDTO();
                Result.Add(visitorDTO);
            }

            return Ok(Result);
        }

        // GET api/<VisitorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitorDTO>> Get(int id)
        {
            logger.LogInformation($"GET Visitor {id} API call - {DateTime.Now.ToShortTimeString()}");

            var queryResult = await context.GetVisitorById(id);

            if (queryResult == null)
            {
                logger.LogWarning($"NOTFOUND Visitor {id} - {DateTime.Now.ToShortTimeString()}");
                return NotFound();
            }

            var result = await queryResult.ConvertToDTO();

            return Ok(result);
        }

        // POST api/<VisitorController>
        [HttpPost]
        public async Task<ActionResult<VisitorDTO>> Post([FromBody] VisitorDTO newVisitor)
        {
            logger.LogInformation($"POST Visitor {newVisitor.Id} - {DateTime.Now.ToShortTimeString()}");

            Visitor result;
            if (ModelState.IsValid)
            {
                result = await context.AddVisitor(new Visitor(newVisitor));         
            }
            else
            {
                logger.LogInformation($"BADREQUEST Modelstate not valid - Visitor {newVisitor.Id} {DateTime.Now.ToShortTimeString()}");
                return BadRequest();
            }

            return Ok(await result.ConvertToDTO());
        }

        // PUT api/<VisitorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] VisitorDTO visitor)
        {
            logger.LogInformation($"PUT Visitor {id} - {DateTime.Now.ToShortTimeString()}");

            Visitor ConvertResult = new(visitor);

            var result = await Task.FromResult(context.UpdateVisitor(ConvertResult));

            if (result == null)
            {
                logger.LogInformation($"NOTFOUND Visitor {id} - {DateTime.Now.ToShortTimeString()}");
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<VisitorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            logger.LogInformation($"DELETE Visitor {id} - {DateTime.Now.ToShortTimeString()}");
            await context.DeleteVisitor(id);

            return Ok();
        }

        //[HttpGet("{SearchTerm}")]
        //public async Task<ActionResult<List<Visitor>>> Search(string SearchTerm)
        //{
        //    logger.LogInformation($"SEARCH Visitors by NAME - {DateTime.Now.ToShortTimeString()}");
        //    var result = await context.SearchVisitorsByName(SearchTerm);

        //    if (result == null)
        //    {
        //        logger.LogInformation($"NOTFOUND Visitors With {SearchTerm} - {DateTime.Now.ToShortTimeString()}");
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}
    }
}
