using AutoMapper;
using Event.Application.Events.Commands.CreateEvent;
using Event.Application.Events.Commands.DeleteEvent;
using Event.Application.Events.Commands.UpdateEvent;
using Event.Application.Events.Queries.GetEventById;
using Event.Application.Events.Queries.GetEvents;
using Event.WebApi.Models.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Event.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EventController : BaseController
    {
        private readonly IMapper _mapper;
        public EventController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<EventLookupDto>>> Get()
        {
            var query = new GetEventsQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDetailsDto>> Get(Guid id)
        {
            var query = new GetEventByIdQuery(id);
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Speaker")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateEventDto createPostDto)
        {
            var command = _mapper.Map<CreateEventCommand>(createPostDto);
            command.SpeakerId = UserId.ToString();
            var eventId = await Mediator.Send(command);
            return Ok(eventId);
        }
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody]  UpdateEventDto eventDto)
        {
            var command = _mapper.Map<UpdateEventCommand>(eventDto);
            command.SpeakerId = UserId.ToString();
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteEventCommand(UserId.ToString(), id);
            await Mediator.Send(command);
            return NoContent();
        }
        
    }
}  
