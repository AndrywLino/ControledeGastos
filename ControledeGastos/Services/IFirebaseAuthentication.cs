using System;
using System.Threading.Tasks;

namespace ControledeGastos.Services
{
    public interface IFirebaseAuthentication
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);

        bool SignOut();

        bool IsSignIn();
    }
}
