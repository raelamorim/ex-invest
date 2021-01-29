using System.Collections.Generic;
using ExInvest.GeradorSaldo.Models;
using GeradorMassa.Models;
using System.Linq;

namespace gerador_saldo.Models
{
    public class Teste
    {
        List<Saldo> saldos = new List<Saldo>();
        List<Cliente> clientes = new List<Cliente>();
        public object ListaSaldos(double valorSaldo) {
            return  from saldo in saldos 
                    join cliente in clientes 
                    on saldo.CPF equals cliente.CPF
                    where saldo.ValorBruto > valorSaldo
                    select  new  { Nome = cliente.Nome, ValorSaldo = saldo.ValorBruto};

        }
    }
}