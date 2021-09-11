using System;
using System.Threading.Tasks;

namespace ControledeGastos.Services
{
    public interface IFirebaseAuthentication
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);

        Task<string> CreatAccountAsync(string email, string password);

        Task<bool> SendResetPasswordAsync(string email);

        bool SignOut();

        bool IsSignIn();

        string GetUserId();
    }
}
