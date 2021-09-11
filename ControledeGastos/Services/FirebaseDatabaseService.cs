using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControledeGastos.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Forms;

namespace ControledeGastos.Services
{
    public class FirebaseDatabaseService
    {
        private static IFirebaseAuthentication _auth;
        static FirebaseClient firebase = new FirebaseClient("https://controle-de-gastos-322513-default-rtdb.firebaseio.com");

        public FirebaseDatabaseService()
        {
            _auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        public static async Task<bool> AddUser(UserModel user)
        {
            try
            {
                var resp = await firebase.Child("Users").PostAsync(user);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static async Task<bool> AddTrade(TradeModel trade)
        {
            try
            {
                string uid = _auth.GetUserId();
                var resp = await firebase.Child("Trades").Child(uid).PostAsync(trade);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TradeModel>> GetTrades()
        {
            string uid = _auth.GetUserId();
            return (await firebase.Child("Trades").Child(uid).OnceAsync<TradeModel>())
                .Select(item => new TradeModel
                {
                    Titulo = item.Object.Titulo,
                    Valor = item.Object.Valor,
                    Parcelas = item.Object.Parcelas,
                    Tipo = item.Object.Tipo,
                    LabelColor = item.Object.LabelColor,
                }).ToList();
        }
    }
}
