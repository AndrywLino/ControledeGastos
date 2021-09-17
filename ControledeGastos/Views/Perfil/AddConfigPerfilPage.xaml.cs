using ControledeGastos.ViewModels;
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
            BindingContext = new AddPerfilConfigViewModel();
        }

        //int i = 0;
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