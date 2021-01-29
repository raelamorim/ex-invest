namespace ExInvest.Clientes.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int Agencia { get; set; }
        public int Conta { get; set; }
        public int DAC { get; set; }
    }
}
