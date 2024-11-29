using Azure;
using Microsoft.EntityFrameworkCore;
using System_Erp.Data;
using System_Erp.Dto;
using System_Erp.Model;
using System_Erp.Services.SenhaService;

namespace System_Erp.Services.AuthService
{
    public class UsuarioAuthService : IUsuarioAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISenhaService _senhaService;

        public UsuarioAuthService(ApplicationDbContext context, ISenhaService senhaService)
        {
            _context = context;
            _senhaService = senhaService;
        }

       public async Task<Resposta<string>> Login(LoginDto loginDto)
        {
            Resposta<string> respostaServico = new Resposta<string>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(userBanco => userBanco.Email == loginDto.Email);
                if (usuario == null)
                {
                    respostaServico.Mensagem = "Credenciais inválidas!";
                    respostaServico.Status = false;
                    return respostaServico;
                }
                if (!_senhaService.VerificaSenhaHash(loginDto.Senha, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    respostaServico.Mensagem = "Credenciais inválidas";
                    respostaServico.Status = false;
                    return respostaServico;
                }
                var token = _senhaService.CriarToken(usuario);
                respostaServico.Dados = token;
                respostaServico.Mensagem = "Usuário logado com sucesso!";
            }
            catch (Exception error)
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = error.Message;
                respostaServico.Status = false;
            }
            return respostaServico;
        }

        public async Task<Resposta<RegistrarDto>> Registrar (RegistrarDto registrarDto)        {
            Resposta <RegistrarDto> respostaServico = new Resposta<RegistrarDto>();
            try
            {
                if (await VerificaEmailJaCadastrado(registrarDto))
                {
                    respostaServico.Dados = null;
                    respostaServico.Status = false;
                    respostaServico.Mensagem = "Usuário já cadastrado";
                    return respostaServico;
                }
                _senhaService.CriarSenhaHash(registrarDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                UsuarioModel usuario = new UsuarioModel()
                {
                    Nome = registrarDto.Nome,
                    Sobrenome = registrarDto.Sobrenome,
                    Email = registrarDto.Email,
                    NumeroTelefone = registrarDto.NumeroTelefone,
                    Peso = registrarDto.Peso,
                    Altura = registrarDto.Altura,
                    Endereco = registrarDto.Endereco,
                    CargoDoUsuario = Enum.CargoUsuario.Paciente,
                    PasswordHash = senhaHash,
                    PasswordSalt = senhaSalt,
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                respostaServico.Mensagem = "Usuário criado com sucesso";
                respostaServico.Status = true;
                respostaServico.Dados = registrarDto;
            }
            catch (Exception error)
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = error.Message;   
                respostaServico.Status = false;
            }

            return respostaServico;
        }

        public async Task<bool> VerificaEmailJaCadastrado(RegistrarDto registrarDto)
        {
            return await _context.Usuarios.AnyAsync(userbanco => userbanco.Email == registrarDto.Email);
        }
    }
}