using System;
using System.Threading.Tasks;
using ControledeGastos.Services;
using Xamarin.Forms;

namespace ControledeGastos.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        #region View

        public Command SendEmailCommand { get; }

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

        #endregion

        #region Constructor

        private IFirebaseAuthentication _auth;

        public ForgotPasswordViewModel()
        {
            _auth = DependencyService.Get<IFirebaseAuthentication>();
            SendEmailCommand = new Command(async () => await SendEmailAsync());
            Title = "Recuperar senha";
        }

        #endregion

        #region Commands

        private async Task SendEmailAsync()
        {
            bool confirm = await _auth.SendResetPasswordAsync(StrEmail);

            if (confirm)
                await App.Current.MainPage.DisplayAlert("Sucesso!", "Nova senha enviada para o seu email, por favor verifique a caixa de spam.", "Ok");
            else
                await App.Current.MainPage.DisplayAlert("Erro!", "Algo deu errado, tente novamente mais tarde.", "Ok");

            await App.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}
