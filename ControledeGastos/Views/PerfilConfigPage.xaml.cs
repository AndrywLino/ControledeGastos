using System;
using System.Collections.Generic;
using ControledeGastos.ViewModels;
using Xamarin.Forms;

namespace ControledeGastos.Views
{
    public partial class PerfilConfigPage : ContentPage
    {
        public PerfilConfigPage()
        {
            InitializeComponent();
            BindingContext = new PerfilConfigViewModel();
        }
    }
}
