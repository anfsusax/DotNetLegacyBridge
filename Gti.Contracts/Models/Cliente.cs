using System;
using System.Runtime.Serialization;

namespace Gti.Contracts.Models
{
    [DataContract]
    public class Cliente
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Email { get; set; }

        // Adicione outras propriedades conforme necessário
    }
}