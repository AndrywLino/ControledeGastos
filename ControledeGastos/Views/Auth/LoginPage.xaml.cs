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

        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            //var ret = PasswordForceService.GetScorePassword(_vm.EntSenha);
            //chamar função de criar login
            //string token = await _auth.CreatAccountAsync(_vm.EntEmail, _vm.EntSenha);

        }
    }
}
