using EMS_BLL;
using EMS_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagementSystem.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index(int EventID)
        {
            RegistrationService rs = new RegistrationService(); // Creating an instance of the EventService class

            var registrations = rs.GetRegistrations(EventID); // Calling the GetRegistrations() method in RegistrationService. The result is stored in the 'registrations' variable.

            // This command below returns a View called "Index" and passes the list of events as a model to this view.
            // MVC then renders the view based on this model, which means the view will have access
            // to the event data for display in the user interface.

            TempData["EventID"] = EventID;
            return View(registrations);
        }


        public ActionResult CreateRegistration()
        {
            Registration registration = null; //Here, it is a reference of registration
            int EventID = (int)TempData["EventID"];

            if (EventID != 0)
                registration = new Registration() {  EventID = EventID }; // here the reference is becoming an object

            return View(registration);
        }

        [HttpPost]
        public ActionResult CreateRegistration(Registration registration)
        {

            return null;
            // call business logic layer AddRegistration()
        }

    }
}