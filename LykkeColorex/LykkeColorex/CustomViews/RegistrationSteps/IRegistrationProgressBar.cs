using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public interface IRegistrationProgressBar
    {
        Task MoveCurrentStepBy(int steps);
        void Setup(int count, int currentStepIndex);
    }
}
