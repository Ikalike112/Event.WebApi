using Event.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Event.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IEventDbContext _eventDb;

        public UpdateEventCommandHandler(IEventDbContext eventDb)
        {
            _eventDb = eventDb;
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var entity =
                await _eventDb.Events.Include(s => s.Speaker)
                .FirstOrDefaultAsync(ev => ev.Id == request.Id, cancellationToken);
            if (entity == null || entity.Speaker.Id != request.SpeakerId)
            {
                throw new Exception($"Event with Id {request.Id} not found");
            }
            entity.Description = request.Description;
            entity.Name = request.Name;
            entity.Venue = request.Venue;
            entity.DateEvent = request.DateEvent;
            await _eventDb.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }
}
