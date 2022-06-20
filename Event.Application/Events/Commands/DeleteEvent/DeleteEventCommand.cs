using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Events.Commands.DeleteEvent
{
    public record DeleteEventCommand(string SpeakerId, Guid Id) : IRequest;

}
