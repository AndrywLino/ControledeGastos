using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ControledeGastos.Services;
using ControledeGastos.Views;

namespace ControledeGastos
{
    public partial class App : Application
    {
        IFirebaseAuthentication _auth;
        public App()
        {
            InitializeComponent();

            _auth = DependencyService.Get<IFirebaseAuthentication>();

            if (_auth.IsSignIn())
                MainPage = new LoginPage();
            //MainPage = new AboutPage();
            else
                MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
