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

        bool IsDismissible { get; }

        Entry FirstFocusEntry { get; }

        void ResetState();

        void Minimize();

        void Maximize();

        event EventHandler StepCompleted;
    }
}
