using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Events.Queries.GetEventById
{
    public record GetEventByIdQuery(Guid Id) : IRequest<EventDetailsDto>;
}
