using ControledeGastos.Services;
using ControledeGastos.Views;
using System;
using Xamarin.Forms;

namespace ControledeGastos.ViewModels
{
    public class PerfilConfigViewModel
    {
        #region Constructor

        IFirebaseAuthentication _auth;

        public PerfilConfigViewModel()
        {
            _auth = DependencyService.Get<IFirebaseAuthentication>();
            BtnLogoffCommand = new Command(Logoff);
        }

        #endregion

        #region View

        public Command BtnLogoffCommand { get; }

        #endregion

        #region Commands

        public void Logoff()
        {
            bool logoff = _auth.SignOut();
            if (logoff)
                App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        #endregion
    }
}
