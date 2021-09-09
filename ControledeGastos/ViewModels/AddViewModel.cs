using System;
namespace ControledeGastos.ViewModels
{
    public class AddViewModel : BaseViewModel
    {
        #region Constructor

        public AddViewModel()
        {

        }

        #endregion

        #region View

        private decimal _money;
        public decimal Money
        {
            get => _money;
            set => SetProperty(ref _money, value);
        }


        #endregion

        #region Commands



        #endregion
    }
}
