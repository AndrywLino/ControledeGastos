using ControledeGastos.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControledeGastos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddConfigPerfilPage : ContentPage
    {
        public AddConfigPerfilPage()
        {
            InitializeComponent();
            //BindingContext = new AddPerfilConfigViewModel();
        }

        private void BtnAddEntryValor(object sender, EventArgs e)
        {
            var newEntry = new Entry
            {
                Placeholder = "Digite aqui seu ganho mensal.",
            };
            var newRadioSim = new RadioButton
            {
                Content = "Sim",
            };
            var newRadioNao = new RadioButton
            {
                Content = "Não",
            };
            var newLabel = new Label
            {
                Text = "Gostaria de ser notificado do saldo em alguma data especifica?"
            };
            StacValor.Children.Add(newEntry);
            StacValor.Children.Add(newLabel);
            StacValor.Children.Add(newRadioSim);
            StacValor.Children.Add(newRadioNao);
        }

        private void BtnAddEntryCartao(object sender, EventArgs e)
        {
            var newEntry = new Entry
            {
                Placeholder = "Apelido para Cartão",
            };
            var newDate = new DatePicker
            {
                Date = DateTime.Now,
            };
            StacCartao.Children.Add(newEntry);
            StacCartao.Children.Add(newDate);
        }

        private void BtnAddSalvar(object sender, EventArgs e)
        {
            foreach (var item in StacValor.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry entry = (Entry)item;
                    var teste = entry.Text;
                }
            }
        }
    }
}