using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControledeGastos.Services;
using ControledeGastos.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControledeGastos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel _vm;
        IFirebaseAuthentication _auth;

        public LoginPage()
        {
            InitializeComponent();
            _auth = DependencyService.Get<IFirebaseAuthentication>();
            this.BindingContext = _vm = new LoginViewModel();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            string token = await _auth.LoginWithEmailAndPassword(_vm.EntEmail, _vm.EntSenha);
            if (token != string.Empty)
            {
                App.Current.MainPage = new AboutPage();
            }
            else
            {
                ShowError();
            }
        }

        private async void ShowError()
        {
            await DisplayAlert("Authentication Failed", "Email or password are incorrect. Try again!", "OK");
        }
    }
}
