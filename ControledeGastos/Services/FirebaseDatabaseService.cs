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
        static FirebaseClient firebase = new FirebaseClient(FirebaseStringConnection.Conexao(),
            new FirebaseOptions
            {
                OfflineDatabaseFactory = (t, s) => new OfflineDatabase(t, s),
                AuthTokenAsyncFactory = async () => await Task.FromResult(await _auth.GetUserTokenAsync()),
            });

        //static FirebaseClient firebase = new FirebaseClient(FirebaseStringConnection.Conexao());

        static RealtimeDatabase<TradeModel> _tradeDB;

        public FirebaseDatabaseService()
        {
            _auth = DependencyService.Get<IFirebaseAuthentication>();
            _uid = _auth.GetUserId();

            _tradeDB = firebase.Child("Trades").Child(_uid).AsRealtimeDatabase<TradeModel>("", "", StreamingOptions.LatestOnly, InitialPullStrategy.MissingOnly, true);
            _tradeDB.PullAsync();
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
                //var resp = await firebase.Child("Trades").Child(_uid).PostAsync(trade);
                var key = _tradeDB.Post(trade);
                trade.TradeId = key;
                _tradeDB.Put(key, trade);
            }
            catch
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        public static async Task<bool> UpdateTrade(string key, TradeModel trade)
        {
            try
            {
                _tradeDB.Put(key, trade);
            }
            catch
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        public static async Task<bool> DeleteTrade(string key)
        {
            try
            {
                _tradeDB.Delete(key);
            }
            catch
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        public async Task<List<TradeModel>> GetTrades()
        {
            try
            {
                //await _tradeDB.PullAsync();

                List<TradeModel> trade = new List<TradeModel>();

                var result = _tradeDB.Once().Select(x => x.Object).ToList();
                await Task.Run(() => result.ForEach(delegate (TradeModel tm)
                {
                    trade.Add(tm);
                }));

                return await Task.FromResult(trade);

                //foreach (var item in _tradeDB.Once())
                //{
                //    trade.Add(item.Object);
                //}


                //var tradeDB = firebase.Child("Trades").Child(_uid).AsRealtimeDatabase<TradeModel>("", "", StreamingOptions.Everything, InitialPullStrategy.Everything, true).Once();

                //return (await firebase.Child("Trades").Child(_uid).OnceAsync<TradeModel>())
                //    .Select(item => new TradeModel
                //    {
                //        Titulo = item.Object.Titulo,
                //        Valor = item.Object.Valor,
                //        Parcelas = item.Object.Parcelas,
                //        Tipo = item.Object.Tipo,
                //        LabelColor = item.Object.LabelColor,
                //    }).ToList();

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
