using System;
using System.Collections.Generic;
using ControledeGastos.ViewModels;
using Xamarin.Forms;

namespace ControledeGastos.Views.Auth
{
    public partial class NewUserPage : ContentPage
    {
        public NewUserPage()
        {
            InitializeComponent();
            BindingContext = new NewUserViewModel();
        }
    }
}
