using System;

namespace ExInvest.GeradorSaldo.Models
{
    public class Saldo
    {
        public string CPF { get; set; }
        public string Tipo => "Saldo#" + IdFundo;
        public int IdFundo { get; set; }
        public string DataReferencia => DateTime.Now.ToString("dd/MM/yyyy");
        public double ValorBruto { get; set; }
        public double ImpostoDevido => Math.Round(ValorBruto * 0.15, 2);
        public double ValorLiquido => Math.Round(ValorBruto - ImpostoDevido, 2);
    }
}