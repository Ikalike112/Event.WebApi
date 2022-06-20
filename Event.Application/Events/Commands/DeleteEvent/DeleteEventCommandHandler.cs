using Event.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Event.Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IEventDbContext _eventsDb;
        public DeleteEventCommandHandler(IEventDbContext eventsDb)
        {
            _eventsDb = eventsDb;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var entity = _eventsDb.Events.Include(s => s.Speaker).Where(x => x.Id == request.Id).FirstOrDefault();
            if (entity == null || entity.Speaker.Id != request.SpeakerId)
            {
                throw new Exception($"Event with Id {request.Id} not found");
            }
            _eventsDb.Events.Remove(entity);
            await _eventsDb.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
