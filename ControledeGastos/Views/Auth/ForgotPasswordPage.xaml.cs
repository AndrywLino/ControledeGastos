using System;
using System.Collections.Generic;
using ControledeGastos.Services;
using ControledeGastos.ViewModels;
using Xamarin.Forms;

namespace ControledeGastos.Views
{
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();

            BindingContext = new ForgotPasswordViewModel();
        }

    }
}
