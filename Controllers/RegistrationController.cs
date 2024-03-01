using EMS_BLL;
using EMS_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace EventManagementSystem.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult Index(int EventID)
        {
            RegistrationService rs = new RegistrationService(); // Creating an instance of the EventService class

            var registrations = rs.GetRegistrations(EventID); // Calling the GetRegistrations() method in RegistrationService. The result is stored in the 'registrations' variable.

            TempData["EventID"] = EventID; // populating tempdata with the current eventID
            return View(registrations);
        }

        // CreateRegistration adds an event participant from the event page. When is is not necessary to select the event in the dropdown.
        public ActionResult CreateRegistration()
        {
            Registration registration = null; //Here, it is a reference of registration
            int EventID = (int)TempData["EventID"];

            if (EventID != 0)
                registration = new Registration()
                {
                    EventID = EventID, // populate the form with the TempData (= EventID) so the user doesn't need to fill it.
                    RegistrationDate = DateTime.Now, // populate the field with the current datetime
                }; // here the reference is becoming an object

            return View(registration);
        }

        [HttpPost]
        public ActionResult CreateRegistration(Registration registration)
        {
            // calling business logic
            RegistrationService es = new RegistrationService();
            if (es.AddRegistrationServices(registration))
            {
                //ViewBag.Message = "Registered successfully";
                return RedirectToAction("Index", new { EventID = registration.EventID });
            }
            else
            {
                return View(registration);
                //ViewBag.Message = "Failed to register"; 
            }

        }


        // CreateRegistrationByEvent add event participant whehn the user navigate from the index and use the dropdown to select the event.
        public ActionResult CreateRegistrationByEvent()
        {

            // Load all registered events in the system
            EventService es = new EventService();
            List<Event> events = es.GetEvents(); // 'events' stores the list of events

            // ViewBag passes the events to the view
            // Each item will be displayed with the event name, and the value will be EventID
            ViewBag.EventList = new SelectList(events, "EventID", "EventName");

            // Passing the instance of Registration to the view
            //Displaying an empty form for the user to fill in with new data.
            return View(new Registration());
        }


        [HttpPost]
        public ActionResult CreateRegistrationByEvent(Registration registration)
        {
            if (ModelState.IsValid)
            {
                //calling business logic if it is valid
                RegistrationService registrationService = new RegistrationService();
                registrationService.AddRegistrationServices(registration);
                return RedirectToAction("Index", new { EventID = registration.EventID });
            }
            else
            {
                return View(registration);
            }
        }


        public ActionResult EditRegistrationView(int registrationID)
        {
            RegistrationService rs = new RegistrationService();
            int EventID = (int)TempData["EventID"];
            var registrations = rs.GetRegistrations(EventID).Find(x => x.RegistrationID == registrationID); // 'find' gets only that particular EventID
            return View(registrations);
        }


        [HttpPost]
        public ActionResult EditRegistrationView(Registration registration)
        {
            RegistrationService es = new RegistrationService();
            if (es.UpdateRegistrationService(registration))
            {
               
                return RedirectToAction("Index", new { EventID = registration.EventID });
                // Later I will change it to show this message and use the button back to the list
                //ViewBag.Message = "Register was updated successfully";
            }

            return View();

            //return null; // first return null to do not see the error
        }

        public ActionResult DeleteRegistration(int registrationID)
        {
            RegistrationService rs = new RegistrationService();
            int EventID = (int)TempData["EventID"];
            var registration = rs.GetRegistrations(EventID).Find(x => x.RegistrationID == registrationID);
            if (rs.DeleteRegistrationService(registrationID, registration.EventID))
            {
                return RedirectToAction("Index", new { EventID = EventID });
            }

            return null;
        }




    }
}