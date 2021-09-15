﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControledeGastos.Models;
using Firebase.Database;
using Firebase.Database.Offline;
using Firebase.Database.Query;
using Xamarin.Forms;

namespace ControledeGastos.Services
{
    public class FirebaseDatabaseService
    {
        private static IFirebaseAuthentication _auth;
        private static string _uid;
        static FirebaseClient firebase = new FirebaseClient("https://controle-de-gastos-322513-default-rtdb.firebaseio.com",
            new FirebaseOptions
            {
                OfflineDatabaseFactory = (t, s) => new OfflineDatabase(t, s),
                AuthTokenAsyncFactory = async () => await _auth.GetUserTokenAsync()
            });

        public FirebaseDatabaseService()
        {
            _auth = DependencyService.Get<IFirebaseAuthentication>();
            _uid = _auth.GetUserId();
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
                var resp = await firebase.Child("Trades").Child(_uid).PostAsync(trade);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TradeModel>> GetTrades()
        {
            var teste = firebase.Child("Trades").Child(_uid).AsRealtimeDatabase<TradeModel>("", "", StreamingOptions.LatestOnly, InitialPullStrategy.MissingOnly, true);

            return null;

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
    }
}
