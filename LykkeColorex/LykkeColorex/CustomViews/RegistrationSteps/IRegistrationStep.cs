using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public interface IRegistrationStep
    {
        Task<bool> Validate();

        void Cleanup();

        void Minimize();

        void Maximize();
    }
}
