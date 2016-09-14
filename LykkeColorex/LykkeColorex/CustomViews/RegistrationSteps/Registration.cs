using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public class Registration
    {
        public static readonly Type[] Steps = {typeof(EmailStep), typeof(PasswordStep)};
    }
}
