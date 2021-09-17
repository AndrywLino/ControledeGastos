using System;
namespace ControledeGastos.Models
{
    public class UserModel
    {
        public string Key { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public PerfilUserModel PerfilConfig { get; set; }

    }
}
