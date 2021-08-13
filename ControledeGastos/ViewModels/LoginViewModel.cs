using ControledeGastos.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ControledeGastos.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
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
    }
}
