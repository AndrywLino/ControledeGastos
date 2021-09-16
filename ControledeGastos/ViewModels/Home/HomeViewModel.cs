using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ControledeGastos.Models;
using ControledeGastos.Services;
using ControledeGastos.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ControledeGastos.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Constructor

        FirebaseDatabaseService _fbService = new FirebaseDatabaseService();

        public HomeViewModel()
        {
            AddPageCommand = new Command(PageAddCommand);
            PageAppearingCommand = new Command(Appearing);
            RefreshCommand = new Command(Refresh);
            DeleteCommand = new Command<TradeModel>((TradeModel) => OnDeleteCommand(TradeModel));
            EditCommand = new Command<TradeModel>((TradeModel) => OnEditCommand(TradeModel));
        }

        public void Refresh()
        {
            Appearing();
            IsRefreshing = false;
            OnPropertyChanged();

        }
        private async void Appearing()
        {
            UserDialogs.Instance.ShowLoading("Carregando Lista, Aguarde.");
            Items = new ObservableCollection<TradeModel>();
            List<TradeModel> trades = await _fbService.GetTrades();
            LblSaldo = await Saldo(trades);
            OnPropertyChanged(nameof(LblSaldo));
            await PopularItemsAsync(trades);
            UserDialogs.Instance.HideLoading();
        }

        private async Task PopularItemsAsync(List<TradeModel> trades)
        {
            await Task.Run(() => trades.ForEach(delegate (TradeModel trade)
            {
                Items.Add(trade);
                OnPropertyChanged(nameof(Items));
            }));
        }

        private async Task<decimal> Saldo(List<TradeModel> trades)
        {
            decimal saldo = 0;

            await Task.Run(() => trades.ForEach(delegate (TradeModel trade)
            {
                saldo += trade.Valor;
            }));

            if (saldo <= 0)
                LblColorSaldo = "Red";
            else
                LblColorSaldo = "Green";
            OnPropertyChanged(nameof(LblColorSaldo));

            return saldo;
        }
        #endregion

        #region View

        public Command AddPageCommand { get; }
        public Command PageAppearingCommand { get; }
        public Command RefreshCommand { get; }

        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                if (_isRefreshing == value)
                    return;
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TradeModel> _items;
        public ObservableCollection<TradeModel> Items
        {
            get => _items;
            set
            {
                if (value == _items)
                    return;
                _items = value;
                OnPropertyChanged(nameof(_items));
            }
        }

        private decimal _lblSaldo;
        public decimal LblSaldo
        {
            get => _lblSaldo;
            set
            {
                if (value == _lblSaldo)
                    return;
                _lblSaldo = value;
                OnPropertyChanged(nameof(_lblSaldo));
            }
        }

        private string _lblColorSaldo;
        public string LblColorSaldo
        {
            get => _lblColorSaldo;
            set
            {
                if (value == _lblColorSaldo)
                    return;
                _lblColorSaldo = value;
                OnPropertyChanged(nameof(_lblColorSaldo));
            }
        }

        #endregion

        #region Commands

        private async void PageAddCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddPage));
        }

        private void OnDeleteCommand(TradeModel trade)
        {
            Task.FromResult(FirebaseDatabaseService.DeleteTrade(trade.TradeId));
            Refresh();
        }

        public void OnEditCommand(TradeModel trade)
        {
            var jsonTrade = JsonConvert.SerializeObject(trade);
            Task.FromResult(Shell.Current.GoToAsync($"{nameof(AddPage)}?Content={jsonTrade}"));
        }

        #endregion
    }
}
