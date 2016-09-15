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
        public abstract Task<bool> Validate();
        public abstract void Cleanup();
        public abstract bool IsDismissible { get; set; }
        public abstract void Minimize();
        public abstract void Maximize();
    }
}
