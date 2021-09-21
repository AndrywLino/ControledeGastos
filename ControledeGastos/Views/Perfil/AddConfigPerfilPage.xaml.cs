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
        private int _page = 1;
        public AddConfigPerfilPage()
        {
            InitializeComponent();
            //BindingContext = new AddPerfilConfigViewModel();
        }

        private void BtnAddEntryValor(object sender, EventArgs e)
        {
            CriarValor();
        }

        private void CriarValor()
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
            var newLabel1 = new Label
            {
                Text = "Informe a Data."
            };
            var newDate = new DatePicker
            {
                Date = DateTime.Now,
            };
            StackValor.Children.Add(newEntry);
            StackValor.Children.Add(newRadioSim);
            StackValor.Children.Add(newRadioNao);
            StackValor.Children.Add(newLabel1);
            StackValor.Children.Add(newDate);

            //teste
        }

        private void BtnAddEntryCartao(object sender, EventArgs e)
        {
            CriarCartao();
        }

        private void CriarCartao()
        {
            var newEntry = new Entry
            {
                Placeholder = "Apelido para Cartão",
            };
            var newDate = new DatePicker
            {
                Date = DateTime.Now,
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
                Text = "Gostaria de ser notificado do vencimento?"
            };
            var newLabel1 = new Label
            {
                Text = "Qual a data de vencimento do cartão?"
            };
            StackCartao.Children.Add(newEntry);
            StackCartao.Children.Add(newLabel1);
            StackCartao.Children.Add(newDate);
            StackCartao.Children.Add(newLabel);
            StackValor.Children.Add(newRadioSim);
            StackValor.Children.Add(newRadioNao);
        }

        private void BtnAddSalvar(object sender, EventArgs e)
        {
            foreach (var item in StackValor.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry entry = (Entry)item;
                    var teste = entry.Text;
                }
            }
        }

        private void BtnProximo(object sender, EventArgs e)
        {
            if (_page == 1)
            {
                //Page 2
                StackNomes.IsVisible = false;
                StackCartao.IsVisible = true;
                StackValor.IsVisible = false;
                BtnProximoName.IsVisible = true;
                BtnCartaoName.IsVisible = true;
                BtnValorName.IsVisible = false;
                BtnSalvarName.IsVisible = false;
                BtnVoltarName.IsVisible = true;
                CriarCartao();
                _page++;
            }
            else if (_page == 2)
            {
                //Page 3
                StackNomes.IsVisible = false;
                StackCartao.IsVisible = false;
                StackValor.IsVisible = true;
                BtnProximoName.IsVisible = false;
                BtnCartaoName.IsVisible = false;
                BtnValorName.IsVisible = true;
                BtnSalvarName.IsVisible = true;
                BtnVoltarName.IsVisible = true;
                CriarValor();
                _page++;
            }
        }

        private void BtnVoltar(object sender, EventArgs e)
        {
            if (_page == 2)
            {
                //Page 1
                StackNomes.IsVisible = true;
                StackCartao.IsVisible = false;
                StackValor.IsVisible = false;
                BtnProximoName.IsVisible = true;
                BtnCartaoName.IsVisible = false;
                BtnValorName.IsVisible = false;
                BtnSalvarName.IsVisible = false;
                BtnVoltarName.IsVisible = false;
                _page--;
            }
            else if (_page == 3)
            {
                //Page 2
                StackNomes.IsVisible = false;
                StackCartao.IsVisible = true;
                StackValor.IsVisible = false;
                BtnProximoName.IsVisible = true;
                BtnCartaoName.IsVisible = true;
                BtnValorName.IsVisible = false;
                BtnSalvarName.IsVisible = false;
                BtnVoltarName.IsVisible = true;
                _page--;
            }
        }
    }
}