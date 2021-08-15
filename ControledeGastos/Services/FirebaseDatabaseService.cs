using System;
using System.Threading.Tasks;
using ControledeGastos.Models;
using Firebase.Database;
using Firebase.Database.Query;

namespace ControledeGastos.Services
{
    public class FirebaseDatabaseService
    {
        static FirebaseClient firebase = new FirebaseClient("https://controle-de-gastos-322513-default-rtdb.firebaseio.com");

        public static async Task<bool> AddUser(UserModel user)
        {
            try
            {
                var resp = await firebase.Child("Users").PostAsync(user);
                return false;
            }
            catch
            {
                return false;
            }

        }
    }
}
