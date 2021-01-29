namespace ExInvest.Clientes.Dtos
{
    public class PostClienteRequest
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int Agencia { get; set; }
        public int Conta { get; set; }
        public int DAC { get; set; }
    }
}