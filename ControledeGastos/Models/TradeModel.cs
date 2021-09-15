using System;
namespace ControledeGastos.Models
{
    public class TradeModel
    {
        public string TradeId { get; set; }
        public string Titulo { get; set; }
        public int Tipo { get; set; }
        public decimal Valor { get; set; }
        public int Parcelas { get; set; }
        public string LabelColor { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
