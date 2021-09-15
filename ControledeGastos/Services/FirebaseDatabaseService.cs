using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using ControledeGastos.Models;
using Firebase.Database;
using Firebase.Database.Offline;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ControledeGastos.Services
{
    public partial class FirebaseDatabaseService
    {
        private static IFirebaseAuthentication _auth;
        private static string _uid;
        static FirebaseClient firebase = new FirebaseClient("https://controle-de-gastos-322513-default-rtdb.firebaseio.com",
            new FirebaseOptions
            {
                OfflineDatabaseFactory = (t, s) => new OfflineDatabase(t, s),
                AuthTokenAsyncFactory = async () => await Task.FromResult(await _auth.GetUserTokenAsync())
            });

        //static FirebaseClient firebase = new FirebaseClient("https://controle-de-gastos-322513-default-rtdb.firebaseio.com");

        static RealtimeDatabase<TradeModel> _tradeDB;

        public FirebaseDatabaseService()
        {
            _auth = DependencyService.Get<IFirebaseAuthentication>();
            _uid = _auth.GetUserId();

            _tradeDB = firebase.Child("Trades").Child(_uid).AsRealtimeDatabase<TradeModel>("", "", StreamingOptions.Everything, InitialPullStrategy.MissingOnly, true);
            var teste = _tradeDB.AsObservable();
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
                if (Internet())
                {
                    var resp = await firebase.Child("Trades").Child(_uid).PostAsync(trade);
                }
                else
                {
                    var resp = _tradeDB.Post(trade);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TradeModel>> GetTrades()
        {
            try
            {
                if (!Internet())
                {
                    List<TradeModel> trade = new List<TradeModel>();

                    //await Task.Run(() => trades.ForEach(delegate (TradeModel trade)
                    foreach (var item in _tradeDB.Once())
                    {
                        trade.Add(item.Object);
                    }

                    return trade;
                }
                else
                {
                    var tradeDB = firebase.Child("Trades").Child(_uid).AsRealtimeDatabase<TradeModel>("", "", StreamingOptions.Everything, InitialPullStrategy.Everything, true).Once();

                    return (await firebase.Child("Trades").Child(_uid).OnceAsync<TradeModel>())
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
            catch (Exception e)
            {
                var erro = e.Message;

                List<TradeModel> trade = new List<TradeModel>();

                return trade;
            }

        }

        public static bool Internet()
        {
            var internet = Connectivity.NetworkAccess;
            if (internet == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
