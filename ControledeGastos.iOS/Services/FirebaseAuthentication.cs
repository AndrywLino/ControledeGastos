﻿using System;
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

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
            return await user.User.GetIdTokenAsync();
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
    }
}
