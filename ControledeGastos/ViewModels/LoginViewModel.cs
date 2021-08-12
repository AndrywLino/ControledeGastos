using ControledeGastos.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ControledeGastos.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        string StrEmail;

        public string EntEmail
        {
            get => StrEmail;
            set
            {
                if (value == StrEmail)
                    return;
                StrEmail = value;
                OnPropertyChanged(nameof(StrEmail));
            }
        }

        string StrSenha;

        public string EntSenha
        {
            get => StrSenha;
            set
            {
                if (value == StrSenha)
                    return;
                StrSenha = value;
                OnPropertyChanged(nameof(StrSenha));
            }
        }

        public LoginViewModel()
        {
            //LoginCommand = new Command(OnLoginClicked);
        }

        //private async void OnLoginClicked(object obj)
        //{

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
          //  await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        //}
    }
}
