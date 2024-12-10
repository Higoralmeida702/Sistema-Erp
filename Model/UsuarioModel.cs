using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System_Erp.Enum;

namespace System_Erp.Model
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 50 caracteres.")]
        public string Nome { get; set;}

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O sobrenome deve ter entre 2 e 50 caracteres.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [StringLength(100, ErrorMessage = "O endereço não pode exceder 100 caracteres.")]
        
        public string Endereco { get; set; }
        
        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "O número de telefone deve ter entre 10 E 11 dígitos.")]
        public string NumeroTelefone { get; set; }
        
        [Required(ErrorMessage = "É necessario preencher o campo peso")]
        public double Peso { get; set; }

        [Required(ErrorMessage = "É necessario preencher o campo peso")]
        public double Altura { get; set; }
        public string? Alergias { get; set; }
        public DateTime AtualizacaoDeInformacoes { get; set; } = DateTime.Now;
        public DateTime CriacaoConta { get; set; } = DateTime.Now;
        public byte[] PasswordHash {get; set; }
        public byte[] PasswordSalt { get; set; }
        
        public CargoUsuario CargoDoUsuario { get; set; }
        public List<SolicitacaoDeCargo> SolicitacaoCargo { get; set; } = new List<SolicitacaoDeCargo>();
        public List<EspecialidadeMedica> EspecialidadeDoMedico { get; set; } = new List<EspecialidadeMedica>();

    }
}