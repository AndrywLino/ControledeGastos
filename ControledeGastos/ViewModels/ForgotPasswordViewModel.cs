using System;
namespace ControledeGastos.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        string StrEmail;

        public string EntEmail
        {
            get => StrEmail;
            set
            {
                if (value == StrEmail)
                    return;
                StrEmail = value;
                OnPropertyChanged(nameof(StrEmail));
            }
        }

        public ForgotPasswordViewModel()
        {
            Title = "Recuperar senha";
        }
    }
}
