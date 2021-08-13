using System;
using System.Collections.Generic;
using ControledeGastos.ViewModels;
using Xamarin.Forms;

namespace ControledeGastos.Views
{
    public partial class ForgotPasswordPage : ContentPage
    {
        ForgotPasswordViewModel _vm;

        public ForgotPasswordPage()
        {
            InitializeComponent();
            BindingContext  = _vm = new ForgotPasswordViewModel();
        }
    }
}
