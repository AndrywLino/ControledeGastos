using System;
using System.Collections.Generic;
using ControledeGastos.ViewModels;
using ControledeGastos.Views;
using Xamarin.Forms;

namespace ControledeGastos
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
            Routing.RegisterRoute(nameof(AddConfigPerfilPage), typeof(AddConfigPerfilPage));
        }

    }
}
