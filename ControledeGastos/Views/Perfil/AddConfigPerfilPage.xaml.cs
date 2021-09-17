using ControledeGastos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControledeGastos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddConfigPerfilPage : ContentPage
    {
        int i = 0;
        public AddConfigPerfilPage()
        {
            InitializeComponent();
            BindingContext = new AddPerfilConfigViewModel();
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    List<string> texto = new List<string>();
        //    var novoStac = new Entry
        //    {
        //        Text = ,
        //        TextColor = Color.Red,
        //    };
        //    i++;
        //    StacValor.Children.Add(novoStac);
        //}
    }
}