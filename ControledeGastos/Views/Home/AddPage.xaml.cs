using System;
using System.Collections.Generic;
using ControledeGastos.Models;
using ControledeGastos.ViewModels;
using Xamarin.Forms;

namespace ControledeGastos.Views
{
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();
            BindingContext = new AddViewModel();
        }
    }
}
