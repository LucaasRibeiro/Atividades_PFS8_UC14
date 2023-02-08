using Chapter.Interfaces;
using Chapter.Models;
using Chapter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;
using System.Security.Claims;

namespace Chapter.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;

        public LoginController(IUsuarioRepository iUsuarioRepository)
        {
            _iUsuarioRepository = iUsuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                Usuario usuarioBuscado = _iUsuarioRepository.Login(login.Email, login.Senha);
                if (usuarioBuscado == null)
                {
                    return Unauthorized(new { msg = "Email e/ou senha inválidos, teste novamente!" });
                }
                //Define os dados que serão fornecidos no token (Playload)
                var minhasClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.Tipo)
                };
                //Define a chave de acesso ao Token
                var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao"));

                //Define as credenciais do token (Header)
                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                //Gera o token
                var meuToken = new JwtSecurityToken(
                   issuer: "Chapter",
                   audience: "Chapter",
                   claims: minhasClaims,
                   expires: DateTime.Now.AddMinutes(60),
                   signingCredentials: credenciais
                   );
                return Ok(
                      new { token = new JwtSecurityTokenHandler().WriteToken(meuToken) }
                    );


            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
