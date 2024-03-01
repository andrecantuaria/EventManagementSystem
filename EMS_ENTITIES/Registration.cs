using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_ENTITIES
{
    public class Registration
    {
        public int EventID { get; set; }
        public int RegistrationID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}
