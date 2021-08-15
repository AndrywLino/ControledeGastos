using Acr.UserDialogs;
using ControledeGastos.Services;
using ControledeGastos.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControledeGastos.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region View

        public Command LoginCommand { get; }

        public Command ForgotPasswordCommand { get; }

        public Command NewUserCommand { get; }

        string StrEmail;

        public string EntEmail
        {
            get => StrEmail;
            set
            {
                if (value == StrEmail)
                    return;
                StrEmail = value;
                TextChangedCommand.Execute(StrEmail);
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

        #endregion

        #region Constructor

        private IFirebaseAuthentication _auth;

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
            ForgotPasswordCommand = new Command(async () => await ForgotPasswordPushPage());
            NewUserCommand = new Command(async () => await NewUserPushPage());
            _auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        #endregion

        #region Commands

        public Command TextChangedCommand => new Command<string>(async (StrEmail) => await TextChanged(StrEmail));

        private async Task TextChanged(string text)
        {
            var teste = text;
        }

        public async Task LoginCommandAsync()
        {
            UserDialogs.Instance.ShowLoading("Carregando Aguarde.");
            string message = await _auth.LoginWithEmailAndPassword(StrEmail, StrSenha);
            if (message == "Ok")
            {
                App.Current.MainPage = new AppShell();
            }
            else
            {
                ShowError("Atenção!", message);
            }
            UserDialogs.Instance.HideLoading();
        }

        private async Task ForgotPasswordPushPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordPage());
        }

        private async void ShowError(string Title, string Message)
        {
            await App.Current.MainPage.DisplayAlert(Title, Message, "OK");
        }


        private async Task NewUserPushPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.Auth.NewUserPage());
        }

        #endregion
    }
}
