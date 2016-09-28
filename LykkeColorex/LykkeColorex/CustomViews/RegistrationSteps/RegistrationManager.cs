using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.Pages;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public class RegistrationManager
    {
        private RegistrationContext _context;
        private List<RegistrationStepMetadata> _steps;
        private RegistrationStepMetadata CurrentStep;

        private RegistrationStepMetadata NextStep()
        {
            return _steps.FirstOrDefault(x => x.Current == x.Next);
        }
        private RegistrationStepMetadata PreviousStep()
        {
            return _steps.FirstOrDefault(x => x.Current == x.Previous);
        }
    }

    public class RegistrationStepMetadata
    {
        public Type Current { set; get; }
        public Type Previous { set; get; }
        public Type Next { set; get; }
    }
}
