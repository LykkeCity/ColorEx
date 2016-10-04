using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public interface IRegistrationHostPage
    {
        void SetSteps(List<RegistrationStep> steps, int currentStepIndex);
        event EventHandler<double> HeightChanged;
        Task MoveCurrentStepBy(int steps);
        double Height { get; }
    }
}
