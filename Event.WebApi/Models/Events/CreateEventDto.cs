using System;

namespace Event.WebApi.Models.Events
{
    public class CreateEventDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateEvent { get; set; }
        public string Venue { get; set; }
    }
}
