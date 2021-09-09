using System;
using ControledeGastos.Views;
using Xamarin.Forms;

namespace ControledeGastos.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Constructor

        public HomeViewModel()
        {
            _addPageCommand = new Command(AddPageCommand);
        }

        #endregion

        #region View

        public Command _addPageCommand { get; }

        #endregion

        #region Commands
        private async void AddPageCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddPage));
        }
        #endregion
    }
}
