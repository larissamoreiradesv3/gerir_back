using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Gerir.back.Dominios;
using Senai.Gerir.back.Interfaces;
using Senai.Gerir.back.Repositorio;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Gerir.back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController()
        {
            _usuarioRepositorio = new UsuarioRepositorio();
        }
        //receber dados de um formulario em forma de post
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                _usuarioRepositorio.Cadastrar(usuario);

                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //Para que n haja conflito entre os EndPoints devemos definir um caminho para diferencia-los
        [HttpPost("login")]
        public IActionResult Logar(Usuario usuario)
        {
            try
            {
                //verificar se o usuario existe 
                var usuarioexiste = _usuarioRepositorio.Logar(usuario.Email, usuario.Senha);
              
                //Caso não exista o usuário retorna NotFound
                if (usuarioexiste == null)
                    return NotFound();
                //Caso o usuário exista um token é gerado
                var token = GerarJsonWebToken(usuarioexiste);
                //Caso exista o usuário
                return Ok(token);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult MeusDados()
        {
            try
            {
                //Pega as informalções referentes ao usuário 
                var claimusuario = HttpContext.User.Claims;
                //Pega o id do usuario na Claim Jti
                var usuarioid = claimusuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
                //pega as informações do usuário 
                var usuario = _usuarioRepositorio.BuscarPorId(new Guid (usuarioid.Value));

                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult Editar(Usuario usuario)
        {
            try
            {
                //Pega as informalções referentes ao usuário 
                var claimusuario = HttpContext.User.Claims;
                //Pega o id do usuario na Claim Jti
                var usuarioid = claimusuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
                //Atribuo o valor do usuarioid ao id do usuario recebdio 
                usuario.Id = new Guid(usuarioid.Value);

                _usuarioRepositorio.Editar(usuario);
                return Ok(usuario);
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]

        public IActionResult Remover()
        {
            try
            {
                //Pega as informalções referentes ao usuário 
                var claimusuario = HttpContext.User.Claims;
                //Pega o id do usuario na Claim Jti
                var usuarioid = claimusuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
                _usuarioRepositorio.Remover(new Guid(usuarioid.Value));
                return Ok(usuarioid);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        private string GerarJsonWebToken(Usuario usuario)
        {
            //Chave de Segurança 
            var chaveSeguranca = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GerirChaveSeguranca"));
            //Define as credenciais
            var credenciais = new SigningCredentials(chaveSeguranca, SecurityAlgorithms.HmacSha256);
            var data = new[]
            {
                new Claim(JwtRegisteredClaimNames.FamilyName, usuario.Nome),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Tipo),
                new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString())
            };
            var token = new JwtSecurityToken(
                "gerir.com.br",
                "gerir.com.br",
                data,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credenciais
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
