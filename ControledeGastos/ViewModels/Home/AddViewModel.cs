using System;
using ControledeGastos.Models;
using ControledeGastos.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ControledeGastos.ViewModels
{
    [QueryProperty(nameof(Content), nameof(Content))]
    public class AddViewModel : BaseViewModel
    {
        #region View

        private decimal _entValor;
        public decimal EntValor
        {
            get => _entValor;
            set => SetProperty(ref _entValor, value);
        }

        private bool _radioEntrada = false;
        public bool RadioEntrada
        {
            get => _radioEntrada;
            set
            {
                if (value == _radioEntrada)
                    return;
                _radioEntrada = value;
                OnPropertyChanged(nameof(_radioEntrada));

                if (_radioEntrada)
                {
                    _parceladoGrid = false;
                    _buttonsGrid = true;
                    OnPropertyChanged(nameof(ButtonsGrid));
                    OnPropertyChanged(nameof(ParceladoGrid));
                }
            }
        }

        private bool _radioSaida = false;
        public bool RadioSaida
        {
            get => _radioSaida;
            set
            {
                if (value == _radioSaida)
                    return;
                _radioSaida = value;
                OnPropertyChanged(nameof(_radioSaida));

                if (_radioSaida)
                    _parceladoGrid = true;
                OnPropertyChanged(nameof(ParceladoGrid));

            }
        }

        private bool _parceladoGrid = false;
        public bool ParceladoGrid
        {
            get => _parceladoGrid;
            set
            {
                if (value == _parceladoGrid)
                    return;
                _parceladoGrid = value;
                OnPropertyChanged(nameof(_parceladoGrid));
            }
        }

        private bool _parceladoStac = false;
        public bool ParceladoStac
        {
            get => _parceladoStac;
            set
            {
                if (value == _parceladoStac)
                    return;
                _parceladoStac = value;
                OnPropertyChanged(nameof(_parceladoStac));
            }
        }

        private bool _buttonsGrid = false;
        public bool ButtonsGrid
        {
            get => _buttonsGrid;
            set
            {
                if (value == _buttonsGrid)
                    return;
                _buttonsGrid = value;
                OnPropertyChanged(nameof(_buttonsGrid));
            }
        }

        private bool _radioNao;
        public bool RadioNao
        {
            get => _radioNao;
            set
            {
                if (value == _radioNao)
                    return;
                _radioNao = value;
                OnPropertyChanged(nameof(_radioNao));

                if (_radioNao)
                {
                    _parceladoStac = false;
                    OnPropertyChanged(nameof(ParceladoStac));

                    _buttonsGrid = true;
                    OnPropertyChanged(nameof(ButtonsGrid));
                }
            }
        }

        private bool _radioSim;
        public bool RadioSim
        {
            get => _radioSim;
            set
            {
                if (value == _radioSim)
                    return;
                _radioSim = value;
                OnPropertyChanged(nameof(_radioSim));

                if (_radioSim)
                {
                    _parceladoStac = true;
                    OnPropertyChanged(nameof(ParceladoStac));

                    _buttonsGrid = true;
                    OnPropertyChanged(nameof(ButtonsGrid));

                }
            }
        }

        public Command BtnCancelarCommand { get; }

        public Command BtnConfirmarCommand { get; }

        private string _entTitulo;
        public string EntTitulo
        {
            get => _entTitulo;
            set
            {
                if (value == _entTitulo)
                    return;
                _entTitulo = value;
                OnPropertyChanged(nameof(_entTitulo));
            }
        }

        private int _entParcelas;
        public int EntParcelas
        {
            get => _entParcelas;
            set
            {
                if (value == _entParcelas)
                    return;
                _entParcelas = value;
                OnPropertyChanged(nameof(_entParcelas));
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (value == _selectedDate)
                    return;
                _selectedDate = value;
                OnPropertyChanged(nameof(_selectedDate));
            }
        }

        #endregion


        #region Constructor
        string _content = "";
        public string Content
        {
            get => _content;
            set
            {
                _content = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
                ConvertJson(_content);
            }
        }
        public AddViewModel()
        {
            BtnCancelarCommand = new Command(CancelCommand);
            BtnConfirmarCommand = new Command(ConfirmCommand);
            SelectedDate = DateTime.Today;
        }

        private void ConvertJson(string json)
        {
            var trade = JsonConvert.DeserializeObject<TradeModel>(json);
            if(trade.Tipo == 2)
            {
                _parceladoGrid = true;
                _radioSaida = true;
            }
            else if(trade.Tipo == 1)
            {
                _parceladoGrid = false;
                _buttonsGrid = true;
                _radioEntrada = true;
            }
            if (trade.Parcelas > 0)
                _radioSim = true;
            else
                _radioNao = true;
            _entTitulo = trade.Titulo;
            _entValor = trade.Valor;
            _entParcelas = trade.Parcelas;
            _selectedDate = trade.DataCompra;
            OnPropertyChanged(nameof(ParceladoGrid));
            OnPropertyChanged(nameof(RadioSaida));
            OnPropertyChanged(nameof(ButtonsGrid));
            OnPropertyChanged(nameof(RadioEntrada));
            OnPropertyChanged(nameof(RadioSim));
            OnPropertyChanged(nameof(EntTitulo));
            OnPropertyChanged(nameof(EntValor));
            OnPropertyChanged(nameof(EntParcelas));
            OnPropertyChanged(nameof(SelectedDate));
            OnPropertyChanged(nameof(RadioNao));
        }

        #endregion


        #region Commands

        private async void CancelCommand()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void ConfirmCommand()
        {
            decimal valor = EntValor;
            TradeModel trade = new TradeModel();
            if (!String.IsNullOrEmpty(EntTitulo))
                trade.Titulo = EntTitulo;


            if (RadioEntrada)
            {
                trade.Tipo = 1;
                trade.LabelColor = "Green";
            }
            if (RadioSaida)
            {
                trade.Tipo = 2;
                trade.LabelColor = "Red";
                valor = EntValor * (-1);
            }

            if (RadioNao)
                trade.Parcelas = 0;
            if (RadioSim)
                trade.Parcelas = EntParcelas;

            if (EntValor != 0)
                trade.Valor = valor;

            trade.DataCompra = SelectedDate;

            bool success = await FirebaseDatabaseService.AddTrade(trade);

            if (success)
                await Shell.Current.GoToAsync("..");
        }

        #endregion
    }
}
