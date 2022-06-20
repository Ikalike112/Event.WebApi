using Event.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Event.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler
        : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventDbContext _eventDb;

        public CreateEventCommandHandler(IEventDbContext eventDb)
        {
            _eventDb = eventDb;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            Domain.Event eventToAdd = new()
            {
                Id = Guid.NewGuid(),
                Speaker = _eventDb.Speakers.Find(request.SpeakerId),
                Name = request.Name,
                Description = request.Description,
                DateEvent = request.DateEvent,
                Venue = request.Venue
            };
            await _eventDb.Events.AddAsync(eventToAdd,cancellationToken);
            await _eventDb.SaveChangesAsync(cancellationToken);
            return eventToAdd.Id;

        }
    }
}
