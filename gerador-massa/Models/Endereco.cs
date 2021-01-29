using System;
using Newtonsoft.Json;

namespace GeradorMassa.Models
{
    public class Endereco
    {
        public string EnderecoCompleto { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
    }
}