using System.ComponentModel;
using Xamarin.Forms;
using ControledeGastos.ViewModels;

namespace ControledeGastos.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
