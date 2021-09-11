using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ControledeGastos.Models;
using ControledeGastos.Services;
using ControledeGastos.Views;
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
        }

        #endregion

        #region View

        public Command AddPageCommand { get; }
        public Command PageAppearingCommand { get; }

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

        #endregion

        #region Commands

        private async void PageAddCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddPage));
        }

        private async void Appearing()
        {
            UserDialogs.Instance.ShowLoading("Carregando Lista, Aguarde.");
            Items = new ObservableCollection<TradeModel>();
            List<TradeModel> trades = await _fbService.GetTrades();
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
            //await foreach (var trade in trades.)
            //{
            //    Items.Add(trade);
            //}
        }
        #endregion
    }
}
