
using System;
using EMS_ENTITIES;
using EMS_DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_BLL
{
    public class EventService
    {
        public List<Event> GetEvents()
        {
            List<Event> events = new List<Event>();
            EventRepository er = new EventRepository();
            events = er.GetEvents();
            return events;
        }

        public bool AddEventServices(Event events)
        {
            EventRepository er = new EventRepository();
            return er.AddEvent(events);
        }

        public bool UpdateEventService (Event events)
        {
            EventRepository er = new EventRepository();
            return er.UpdateEvent(events);
        }

        public bool DeleteEventService (int eventID)
        {
            EventRepository er = new EventRepository();
            return er.DeleteEvent(eventID);

        }





    }
}
