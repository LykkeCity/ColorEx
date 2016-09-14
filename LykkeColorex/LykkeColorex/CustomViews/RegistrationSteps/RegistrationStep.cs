using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public abstract class RegistrationStep : ContentView, IRegistrationStep
    {
        public abstract Task<bool> Validate();
    }
}
