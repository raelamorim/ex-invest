using System;

namespace ExInvest.Investimentos.Models
{
    public class Saldo
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdFundo { get; set; }
        public DateTime DataReferencia => DateTime.Now;
        public decimal ValorBruto { get; set; }
        public decimal ImpostoDevido => ValorBruto * 0.15m;
        public decimal ValorLiquido => ValorBruto - ImpostoDevido;
    }
}