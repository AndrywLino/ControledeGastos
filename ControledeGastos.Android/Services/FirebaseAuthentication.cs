using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ControledeGastos.Services;
using Firebase.Auth;

namespace ControledeGastos.Droid.Services
{
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        public bool IsSignIn()
        {
            var user = FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }

        public bool SignOut()
        {
            try
            {
                FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetUserId()
        {
            return FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public async Task<string> GetUserTokenAsync()
        {
            return null;
        }

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdToken(false);

                if (FirebaseAuth.Instance.CurrentUser.IsEmailVerified)
                {
                    return "Ok";
                }
                else
                {
                    await FirebaseAuth.Instance.CurrentUser.SendEmailVerification();
                    return "Email não verificado, cheque sua caixa de email para validar seu acesso.";
                }
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
        }

        public async Task<string> CreatAccountAsync(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                await FirebaseAuth.Instance.CurrentUser.SendEmailVerification();

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
                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
