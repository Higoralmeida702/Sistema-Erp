using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System_Erp.Data;
using System_Erp.Enum;
using System_Erp.Model;
using System_Erp.Services.SenhaService;

namespace System_Erp.Services.AuthService
{
    public class AdministradorAccountService : IAdministradorAccountService
    {

        private readonly ApplicationDbContext _context;
        private readonly ISenhaService _senhaService;

        public AdministradorAccountService(ApplicationDbContext context, ISenhaService senhaService)
        {
            _context = context;
            _senhaService = senhaService;
        }

        public async Task<Resposta<string>> AtribuirCargo(int usuarioId, CargoUsuario novoCargo)
        {
            var  resposta = new Resposta<string>();

            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                resposta.Status = false;
                resposta.Mensagem = "Usuario não encontrado";
                return resposta;
            }

            usuario.CargoDoUsuario = novoCargo;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            resposta.Dados = "Dados salvo com sucesso";
            resposta.Status = true;
            resposta.Mensagem = "Operação realizada com sucesso!";
            return resposta;
        }

        public async Task InicializarAdm()
        {
            if (!_context.Usuarios.Any(u => u.CargoDoUsuario == CargoUsuario.Administrador))
            {
                var admin = new UsuarioModel
                {
                    Nome = "Admin",
                    Sobrenome = "Administrador",
                    Email = "admin@gmail.com",
                    Endereco = "rua",
                    NumeroTelefone = "12345678910",
                    Peso = 120,
                    Altura = 164,
                    CargoDoUsuario = CargoUsuario.Administrador,
                    CriacaoConta = DateTime.Now
                };

                _senhaService.CriarSenhaHash("senhasegura123", out byte[] passwordHash, out byte[] passwordSalt);

                admin.PasswordHash = passwordHash;
                admin.PasswordSalt = passwordSalt;

                _context.Usuarios.Add(admin);
                await _context.SaveChangesAsync();

            }
        }
    }
}