using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public interface IRegistrationContext
    {
        string Email { set; get; }
        string FirstName { set; get; }
        string LastName { set; get; }
        string PhoneNumber { set; get; }
    }
}
