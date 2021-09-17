using ControledeGastos.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ControledeGastos.ViewModels
{
    class AddPerfilConfigViewModel : BaseViewModel
    {
        #region Constructor

        public AddPerfilConfigViewModel()
        {
            BtnAddValorCommand = new Command(AddEntryValor);

        }

        #endregion

        #region View

        public Command BtnAddValorCommand { get; }

        public StackLayout _stacValor;
        public StackLayout StacValor
        {
            get => _stacValor;
            set
            {
                if (_stacValor == value)
                    return;
                _stacValor = value;
                OnPropertyChanged(nameof(_stacValor));
            }
        }

        public List<string> _entValor;
        public List<string> EntValor
        {
            get => _entValor;
            set
            {
                if (_entValor == value)
                    return;
                _entValor = value;
                OnPropertyChanged(nameof(_entValor));
            }
        }

        #endregion


        #region Commands

        public void AddEntryValor()
        {
            var teste = StacValor;
            var novoStac = new StackLayout
            {
                Children =
                {
                    new Entry
                    {
                        Text = "Novo Entry",
                        IsPassword = true,
                    }
                }
            };
            //StacValor.Children.Add(novoStac);
            OnPropertyChanged(nameof(StacValor));
        }

        #endregion
    }
}
