using System;
using System.Collections.Generic;
using System.Text;

namespace ControledeGastos.Models
{
    public class PerfilUserModel
    {
        public string Key { get; set; }
        public string Uid { get; set; }
        public string UserKey { get; set; }
        public List<decimal> UserGanhoMensal { get; set; }
        public List<CartoesModel> UserCartoes { get; set; }
    }
}
