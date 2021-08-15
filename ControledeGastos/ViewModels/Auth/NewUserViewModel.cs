using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ControledeGastos.Models;
using ControledeGastos.Services;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Forms;

namespace ControledeGastos.ViewModels
{
    public class NewUserViewModel : BaseViewModel
    {
        #region View

        string StrName = string.Empty;

        public string EntName
        {
            get => StrName;
            set
            {
                if (value == StrName)
                    return;
                StrName = value;
                OnPropertyChanged(nameof(StrName));
            }
        }

        string StrEmail = string.Empty;

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

        string StrPassword = string.Empty;

        public string EntPassword
        {
            get => StrPassword;
            set
            {
                if (value == StrPassword)
                    return;
                StrPassword = value;
                CheckPasswordForce.Execute(StrPassword);
                OnPropertyChanged(nameof(StrPassword));
            }
        }

        string StrConfirmPassword = string.Empty;

        public string EntConfirmPassword
        {
            get => StrConfirmPassword;
            set
            {
                if (value == StrConfirmPassword)
                    return;
                StrConfirmPassword = value;
                ConfirmPasswordChanged.Execute(StrConfirmPassword);
                OnPropertyChanged(nameof(StrConfirmPassword));
            }
        }

        string StrMessage = "";

        public string LblMessage
        {
            get => StrMessage;
            set
            {
                if (value == StrMessage)
                    return;
                StrMessage = value;
                OnPropertyChanged(nameof(StrMessage));
            }
        }

        string StrPassMessage = "";

        public string LblPassMessage
        {
            get => StrPassMessage;
            set
            {
                if (value == StrPassMessage)
                    return;
                StrPassMessage = value;
                OnPropertyChanged(nameof(StrPassMessage));
            }
        }

        string StrPassColor = "";

        public string LblPassColor
        {
            get => StrPassColor;
            set
            {
                if (value == StrPassColor)
                    return;
                StrPassColor = value;
                OnPropertyChanged(nameof(StrPassColor));
            }
        }

        float NuProgressBarr = 0;

        public float PrbValue
        {
            get => NuProgressBarr;
            set
            {
                if (value == NuProgressBarr)
                    return;
                NuProgressBarr = value;
                OnPropertyChanged(nameof(NuProgressBarr));
            }
        }

        bool IsConfirmVisible = false;

        public bool BtnConfirmVisible
        {
            get => IsConfirmVisible;
            set
            {
                if (value == IsConfirmVisible)
                    return;
                IsConfirmVisible = value;
                OnPropertyChanged(nameof(IsConfirmVisible));
            }
        }

        public Command ConfirmCommand { get; }

        #endregion

        #region Contructor


        private IFirebaseAuthentication _auth;

        public NewUserViewModel()
        {
            _auth = DependencyService.Get<IFirebaseAuthentication>();
            ConfirmCommand = new Command(async () => await ConfirmAsync());
        }



        #endregion

        #region Commands

        public Command ConfirmPasswordChanged => new Command<string>(async (StrConfirmPassword) => await ConfirmPasswordChangedAsync(StrConfirmPassword));
        public Command CheckPasswordForce => new Command<string>(async (StrPassword) => await CheckForcePassword(StrPassword));

        private async Task CheckForcePassword(string strPassword)
        {
            string force = PasswordForceService.GetScorePassword(strPassword).ToString(); ;

            if (force == "Inaceitavel")
            {
                StrPassMessage = "Senha muito Fraca";
                StrPassColor = "Red";
                NuProgressBarr = 0;
            }
            if (force == "Fraca")
            {
                StrPassMessage = "Senha Fraca";
                StrPassColor = "Red";
                NuProgressBarr = 0.3f;
            }
            if (force == "Media")
            {
                StrPassMessage = "Senha Media";
                StrPassColor = "Orange";
                NuProgressBarr = 0.5f;
            }
            if (force == "Forte")
            {
                StrPassMessage = "Senha Forte";
                StrPassColor = "Green";
                NuProgressBarr = 0.8f;
            }
            if (force == "Segura")
            {
                StrPassMessage = "Senha Segura";
                StrPassColor = "Blue";
                NuProgressBarr = 1;
            }

            OnPropertyChanged(nameof(LblPassMessage));
            OnPropertyChanged(nameof(LblPassColor));
            OnPropertyChanged(nameof(PrbValue));
        }

        private async Task ConfirmPasswordChangedAsync(string strConfirmPassword)
        {
            if (StrPassword != strConfirmPassword)
            {
                StrMessage = "As Senhas não coincidem!";
                IsConfirmVisible = false;
            }
            else
            {
                StrMessage = "";
                if (StrPassword != string.Empty && StrName != string.Empty && StrEmail != string.Empty)
                    IsConfirmVisible = true;
                else
                    IsConfirmVisible = false;
            }

            OnPropertyChanged(nameof(LblMessage));
            OnPropertyChanged(nameof(BtnConfirmVisible));
        }

        public async Task ConfirmAsync()
        {
            UserDialogs.Instance.ShowLoading("Carregando Aguarde.");
            string uid = await _auth.CreatAccountAsync(StrEmail, StrPassword);
            UserModel user = new UserModel();
            user.Email = StrEmail;
            user.Uid = uid;
            user.Name = StrName;

            bool success = await FirebaseDatabaseService.AddUser(user);

            if (uid != string.Empty && success)
            {
                await App.Current.MainPage.DisplayAlert("Sucesso!", "Usuario criado com sucesso, acesso sua conta de email para ativar seu acesso.", "Ok");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
                await App.Current.MainPage.DisplayAlert("Erro!", "Algo deu errado, tente novamente.", "Ok");

            UserDialogs.Instance.HideLoading();

        }

        #endregion
    }
}
