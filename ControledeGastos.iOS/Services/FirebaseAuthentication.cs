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

        public string GetUserId()
        {
            return Auth.DefaultInstance.CurrentUser.Uid;
        }

        public async Task<string> GetUserTokenAsync()
        {
            return await Auth.DefaultInstance.CurrentUser.GetIdTokenAsync();
        }

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                if (Auth.DefaultInstance.CurrentUser.IsEmailVerified)
                    return "Ok";
                else
                {
                    await user.User.SendEmailVerificationAsync();
                    return "Email não verificado, cheque sua caixa de email para validar seu acesso.";
                }
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
                await user.User.SendEmailVerificationAsync();

                return user.User.Uid;
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
