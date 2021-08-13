using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using ControledeGastos.Services;
using Foundation;
using UIKit;
using ControledeGastos.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseAuthentication))]
namespace ControledeGastos.iOS.Services
{
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        public bool IsSignIn()
        {
            var user = Auth.DefaultInstance.CurrentUser;
            return user != null;
        }

        public bool SignOut()
        {
            try
            {
                _ = Auth.DefaultInstance.SignOut(out NSError error);
                return error == null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<string> CreatAccountAsync(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.CreateUserAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<bool> SendResetPasswordAsync(string email)
        {
            try
            {
                await Auth.DefaultInstance.SendPasswordResetAsync(email);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
