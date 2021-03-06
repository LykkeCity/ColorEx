﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public abstract class RegistrationStep : ContentView, IRegistrationStep
    {
        public RegistrationStep(IRegistrationContext context)
        {
            _context = context;
        }

        private IRegistrationContext _context;
        public IRegistrationContext Context => _context;

        public abstract Task<bool> Validate();
        public abstract void Initalize();
        public abstract void Cleanup();
        public abstract bool IsDismissible { get; }
        public abstract Entry FirstFocusEntry { get; }
        public abstract void ResetState();
        public abstract Task Minimize();
        public abstract Task Maximize();

        public abstract event EventHandler StepCompleted;
    }
}
