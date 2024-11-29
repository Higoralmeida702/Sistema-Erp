using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace System_Erp.Dto
{
    public class RegistrarDto
    {
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
        
        [Required(ErrorMessage = "É necessario preencher o campo altura")]
        public double Altura { get; set;}

        [Required(ErrorMessage = "Digite uma senha valida")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "É necessario o preenchimento do campo confirmar senha")]
        [Compare("Senha", ErrorMessage = "Senha não coincidem")]
        public string ConfirmarSenha { get; set;}
    }
}