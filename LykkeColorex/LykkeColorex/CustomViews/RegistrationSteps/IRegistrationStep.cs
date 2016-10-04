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

        void Initalize();

        void Cleanup();

        bool IsDismissible { get; }

        Entry FirstFocusEntry { get; }

        void ResetState();

        Task Minimize();

        Task Maximize();

        event EventHandler StepCompleted;
    }
}
