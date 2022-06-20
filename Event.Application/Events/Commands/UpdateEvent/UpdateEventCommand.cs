using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest
    {
        public string SpeakerId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public DateTime DateEvent { get; set; }
    }
}
