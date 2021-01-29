using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GeradorMassa.Models
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string Tipo => "Cadastro";
        public string CPF { get; set; }
        public string DataNascimento { get; set; }
        public string Email { get; set; }
        public IEnumerable<Conta> Contas { get; set; }
        public IEnumerable<Telefone> Telefones { get; set; }
        public IEnumerable<Endereco> Enderecos { get; set; }

        internal string DumpAsJson()
        {
            return JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }
    }
}