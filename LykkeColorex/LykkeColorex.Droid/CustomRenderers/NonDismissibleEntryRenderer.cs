using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;
using LykkeColorex.CustomViews;
using LykkeColorex.Droid.CustomRenderers;
using LykkeColorex.Droid.CustomViews;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;


[assembly: ExportRenderer(typeof(NonDismissibleEntry), typeof(NonDismissibleEntryRenderer))]
namespace LykkeColorex.Droid.CustomRenderers
{
    public class NonDismissibleEntryRenderer : ViewRenderer<NonDismissibleEntry, NonDismissibleEditText>, ITextWatcher, TextView.IOnEditorActionListener
    {
        NonDismissibleEditText _textView;

        public NonDismissibleEntryRenderer()
        {
            AutoPackage = false;
        }


        public void AfterTextChanged(IEditable s)
        {

        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {

        }
        
        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            if (string.IsNullOrEmpty(Element.Text) && s.Length() == 0)
                return;

            ((IElementController)Element).SetValueFromRenderer(Entry.TextProperty, s.ToString());
        }

        public bool OnEditorAction(TextView v, ImeAction actionId, KeyEvent e)
        {
            if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && e.KeyCode == Keycode.Enter))
            {
                Control.ClearFocus();
                InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(WindowToken, 0);
                ((IEntryController)Element).SendCompleted();
            }

            return true;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NonDismissibleEntry> e)
        {
            base.OnElementChanged(e);

            //HandleKeyboardOnFocus = true;

            if (e.OldElement == null)
            {
                _textView = new NonDismissibleEditText(Context);
                _textView.ImeOptions = ImeAction.Done;
                _textView.AddTextChangedListener(this);
                _textView.SetOnEditorActionListener(this);
                if (e.NewElement.IsPin)
                {
                    _textView.SetCursorVisible(false);
                    _textView.SetRawInputType(InputTypes.ClassNumber | InputTypes.NumberVariationPassword);
                    _textView.ImeOptions = ImeAction.ImeMaskAction;
                    _textView.ImeOptions = ImeAction.None;
                    _textView.SetSingleLine(true);
                    _textView.LongClickable = false;
                }
                //_textView.OnKeyboardBackPressed += (sender, args) => _textView.ClearFocus();
                SetNativeControl(_textView);
            }

            _textView.Hint = Element.Placeholder;
            _textView.ShowSoftInputOnFocus = true;
            _textView.Text = Element.Text;

            _textView.FocusChange += delegate(object sender, FocusChangeEventArgs args)
            {

                if (args.HasFocus)
                {
                    ShowKeyboard(_textView);
                }
                else
                {
                    //HideKeyboard(_textView);
                    //Control.ClearFocus();
                }

            };

        }

        

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Entry.PlaceholderProperty.PropertyName)
                Control.Hint = Element.Placeholder;

            base.OnElementPropertyChanged(sender, e);
        }


        internal static void HideKeyboard(NonDismissibleEditText inputView, bool overrideValidation = false)
        {
            if (Forms.Context == null)
                throw new InvalidOperationException("Call Forms.Init() before HideKeyboard");

            using (var inputMethodManager = (InputMethodManager)Forms.Context.GetSystemService(Context.InputMethodService))
            {
                IBinder windowToken = null;

                if (!overrideValidation && !(inputView is NonDismissibleEditText || inputView is TextView || inputView is SearchView))
                    throw new ArgumentException("inputView should be of type EditText, SearchView, or TextView");

                windowToken = inputView.WindowToken;
                if (windowToken != null)
                    inputMethodManager.HideSoftInputFromWindow(windowToken, HideSoftInputFlags.None);
            }
        }

        internal static void ShowKeyboard(NonDismissibleEditText inputView)
        {
            if (Forms.Context == null)
                throw new InvalidOperationException("Call Forms.Init() before ShowKeyboard");

            using (var inputMethodManager = (InputMethodManager)Forms.Context.GetSystemService(Context.InputMethodService))
            {
                if (inputView is NonDismissibleEditText || inputView is TextView || inputView is SearchView)
                {
                    inputMethodManager.ShowSoftInput(inputView, ShowFlags.Forced);
                    inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
                }
                else
                    throw new ArgumentException("inputView should be of type EditText, SearchView, or TextView");
            }
        }
    }
}