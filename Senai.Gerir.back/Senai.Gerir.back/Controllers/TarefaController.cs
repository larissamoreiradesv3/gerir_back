using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Gerir.back.Dominios;
using Senai.Gerir.back.Interfaces;
using Senai.Gerir.back.Repositorio;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController()
        {
            _tarefaRepositorio = new TarefaRepositorio();
        }

        [HttpPost]
        public IActionResult Cadastrar(Tarefa tarefa)
        {
            try
            {
                //Pega o valor do usuario que esta logado
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(
                    c => c.Type == JwtRegisteredClaimNames.Jti
                    );
                //Atribui o usuario a Tabela
                tarefa.UsuarioId = new System.Guid(usuarioid.Value);

                _tarefaRepositorio.Cadastrar(tarefa);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{IdTarefa}")]
        public IActionResult Editar(Guid IdTarefa, Tarefa tarefa)
        {
            try
            {
                var tarefaeditar = HttpContext.User;

                var idusuario = HttpContext.User.Claims.FirstOrDefault(
                     c => c.Type == JwtRegisteredClaimNames.Jti
                     );
                var tarefasolicitada = _tarefaRepositorio.BuscarPorId(IdTarefa);
                if (tarefasolicitada.UsuarioId != new Guid(idusuario.Value))
                    return Unauthorized("Usuário não autorizado para exercer está ação");
                tarefa.Id = IdTarefa;
                _tarefaRepositorio.Editar(tarefa);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete ("{IdTarefa}")]
        public IActionResult Remover(Guid IdTarefa)
        {
            try
            {
                var tarefaremovida = HttpContext.User;
                _tarefaRepositorio.AlterarStatus(IdTarefa);
                var idusuario = HttpContext.User.Claims.FirstOrDefault(
                     c => c.Type == JwtRegisteredClaimNames.Jti
                     );
                var tarefasolicitada = _tarefaRepositorio.BuscarPorId(IdTarefa);
                if (tarefasolicitada.UsuarioId != new Guid(idusuario.Value))
                    return Unauthorized("Usuário não autorizado para exercer está ação"); 
             
                _tarefaRepositorio.Remover(IdTarefa);
                return Ok(IdTarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("alterarstatus/{IdTarefa}")]
        public IActionResult AlterarStatus(Guid IdTarefa)
        {
            try
            {
                _tarefaRepositorio.AlterarStatus(IdTarefa);
                var idusuario = HttpContext.User.Claims.FirstOrDefault(
                     c => c.Type == JwtRegisteredClaimNames.Jti
                     );
                var tarefasolicitada = _tarefaRepositorio.BuscarPorId(IdTarefa);
                if (tarefasolicitada.UsuarioId != new Guid(idusuario.Value))
                    return Unauthorized("Usuário não autorizado para exercer está ação");
                return Ok(IdTarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("listar/{IdUsuario}")]
        public IActionResult ListarTodos(Guid IdUsuario)
        {
            var listatarefas = _tarefaRepositorio.ListarTodos(IdUsuario);
            return Ok(listatarefas);

        }

        [Authorize]
        [HttpGet("buscartarefa/{IdTarefa}")]
        public IActionResult BuscarTarefa(Guid IdTarefa)
        {
            try
            {
                var idusuario = HttpContext.User.Claims.FirstOrDefault(
                    c => c.Type == JwtRegisteredClaimNames.Jti
                    );

                var buscartarefa = _tarefaRepositorio.BuscarPorId(IdTarefa);
                

                if (buscartarefa == null)
                    return NotFound();
                
                if (buscartarefa.UsuarioId != new Guid(idusuario.Value))
                    return Unauthorized("Usuário não autorizado para exercer está ação");
                
                    return Ok(buscartarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




    }
}
