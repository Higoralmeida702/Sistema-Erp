using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System_Erp.Enum;

namespace System_Erp.Model
{
    public class SolicitacaoDeCargo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "É necessario informar qual o id do usuario que solicitara o cargo")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "É necessario informar qual o cargo ira ser solicitado")]
        public CargoUsuario CargoSolicitado { get; set; }
        public DateTime DataSolicitacao { get; set; } = DateTime.Now;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set;}
        
        public UsuarioModel Usuario { get; set; }

    }
}