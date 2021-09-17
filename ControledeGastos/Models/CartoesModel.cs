using System;
using System.Collections.Generic;
using System.Text;

namespace ControledeGastos.Models
{
    public class CartoesModel
    {
        public string Key { get; set; }
        public string PerfilUserKey { get; set; }
        public string CartaoName { get; set; }
        public DateTime CartaoVencimento { get; set; }
        public DateTime CartaoFechamento { get; set; }
    }
}
