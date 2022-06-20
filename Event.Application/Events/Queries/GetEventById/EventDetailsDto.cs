using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Events.Queries.GetEventById
{
    public class EventDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SpeakerName { get; set; }
        public DateTime DateEvent { get; set; }
        public string Venue { get; set; }
    }
}
