using System;

namespace ExInvest.Investimentos.Models
{
    public class Saldo
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdFundo { get; set; }
        public DateTime DataReferencia { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ImpostoDevido { get; set; }
        public decimal ValorLiquido { get; set; }
    }
}