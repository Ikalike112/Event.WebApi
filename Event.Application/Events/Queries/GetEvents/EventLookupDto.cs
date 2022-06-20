using Event.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Events.Queries.GetEvents
{
    public class EventLookupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SpeakerName { get; set; }
    }
}
