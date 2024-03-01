using System;
using EMS_BLL;
using EMS_ENTITIES;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;

namespace EventManagementSystem.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            EventService es = new EventService(); // Creating an instance of the EventService class

            var events = es.GetEvents(); // Calling the GetEvents() method in EventService. The result is stored in the 'events' variable.

            // This command below returns a View called "Index" and passes the list of events as a model to this view.
            // MVC then renders the view based on this model, which means the view will have access
            // to the event data for display in the user interface.
            return View(events);
        }


        public ActionResult CreateEventView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEventView(Event events)
        {
            EventService es = new EventService(); 
            if (es.AddEventServices(events))
            {
                ViewBag.Message = "Event added successfully"; 
            }
            else
            {
                ViewBag.Message = "Failed to add event"; 
            }

            return View(); 
        }


        // This is used to return view with record based on EventID
        public ActionResult EditEventView(int eventID)
        {
            EventService es = new EventService();
            var events = es.GetEvents().Find(x => x.EventID == eventID); // 'find' gets only that particular EventID
            return View(events);
        }


        // This is used to update the record and pass to Service and Respository
        [HttpPost]
        public ActionResult EditEventView(Event events) 
        {
            EventService es = new EventService();
            if(es.UpdateEventService(events))
            {
                ViewBag.Message = "Event was updated successfully";
            }

            return View();

            //return null; // first return null to do not see the error
        }

        public ActionResult DeleteEvent(int eventID)
        {
            EventService es = new EventService();
            if (es.DeleteEventService(eventID))
            {
                return RedirectToAction("Index");
            }
            
            return null;
        }

    }
}
