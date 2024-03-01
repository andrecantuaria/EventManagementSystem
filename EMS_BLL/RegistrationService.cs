using EMS_DAL;
using EMS_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_BLL
{
    public class RegistrationService
    {
        public List<Registration> GetRegistrations(int EventID)
        {
            List<Registration> registrations = new List<Registration>();
            RegistrationRepository rr = new RegistrationRepository();
            registrations = rr.GetRegistrations(EventID);
            return registrations;
        }

        public bool AddRegistrationServices(Registration registrations)
        {
            RegistrationRepository rr = new RegistrationRepository();
            return rr.AddRegistration(registrations);
        }

        public bool UpdateRegistrationService(Registration registrations)
        {
            RegistrationRepository rr = new RegistrationRepository();
            return rr.UpdateRegistration(registrations);
        }

        public bool DeleteRegistrationService(int RegistrationID, int EventID)
        {
            RegistrationRepository rr = new RegistrationRepository();
            return rr.DeleteRegistration(RegistrationID, EventID);
        }

    }
}
