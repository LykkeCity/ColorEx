using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.Pages;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public class RegistrationManager
    {
        private IRegistrationProgressBar _progressBar;
        private IRegistrationHostPage _hostPage;
        private IStickyButton _stickyButton;
        private RegistrationStep _currentStep;
        private List<RegistrationStep> _steps;
        private IRegistrationContext _context;

        public event EventHandler RegistrationCompleted;

        public RegistrationManager(Type startStep, IRegistrationHostPage hostPage, IStickyButton stickyButton, IRegistrationProgressBar progressBar, IRegistrationContext context)
        {
            _hostPage = hostPage;
            _stickyButton = stickyButton;
            _progressBar = progressBar;
            _context = context;

            _steps = ResolveSteps();

            _currentStep = _steps.FirstOrDefault(x => x.GetType() == startStep);
            var currentStepIndex = _steps.IndexOf(_currentStep);

            _progressBar.Setup(_steps.Count, currentStepIndex);
            _hostPage.SetSteps(_steps, currentStepIndex);

            _currentStep.Initalize();
            _currentStep.StepCompleted += CurrentStepOnStepCompleted;
        }

        private async void CurrentStepOnStepCompleted(object sender, EventArgs eventArgs)
        {
            try
            {
                _currentStep.Cleanup();
                _currentStep.StepCompleted -= CurrentStepOnStepCompleted;

                if (_currentStep == _steps.Last())
                {
                    OnRegistrationCompleted();
                    return;
                }

                //_currentStep.Maximize();

                _currentStep = _steps[_steps.IndexOf(_currentStep) + 1];

                _currentStep.Initalize();

                Device.BeginInvokeOnMainThread(() => _currentStep.FirstFocusEntry.Focus());

                await Task.WhenAll(_hostPage.MoveCurrentStepBy(1), _progressBar.MoveCurrentStepBy(1));
                
                _currentStep.StepCompleted += CurrentStepOnStepCompleted;
            }
            catch (Exception e)
            {
                var a = 234;
            }
        }
        
        private List<RegistrationStep> ResolveSteps()
        {
            return new List<RegistrationStep>
            {
                new EmailStep(_stickyButton, _context),
                new EmailConfirmStep(_stickyButton, _context),
                new PasswordStep(_stickyButton, _context),
                new NameStep(_stickyButton, _context)
            };
        }


        protected virtual void OnRegistrationCompleted()
        {
            RegistrationCompleted?.Invoke(this, EventArgs.Empty);
        }
    }

    public class RegistrationStepMetadata
    {
        public Type Current { set; get; }
        public Type Previous { set; get; }
        public Type Next { set; get; }
    }
}
