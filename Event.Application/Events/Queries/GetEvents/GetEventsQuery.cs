using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Events.Queries.GetEvents
{
    public record GetEventsQuery() : IRequest<List<EventLookupDto>>;
}
