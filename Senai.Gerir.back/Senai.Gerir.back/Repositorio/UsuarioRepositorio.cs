using Senai.Gerir.back.Contextos;
using Senai.Gerir.back.Dominios;
using Senai.Gerir.back.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.back.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly GerirContext _context;
        
        public UsuarioRepositorio()
        {
            _context = new GerirContext();
        }

        public Usuario BuscarPorId(Guid id)
        {
            try
            {
                //para buscar o id do usuario usando o metodo Find 
                var usuario = _context.Usuarios.Find(id);
                //após buscar retorna para o usuario 
                return usuario;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            try
            {
                //adiciona um usuario ao DbSet Usuario do contexto
                _context.Usuarios.Add(usuario);
                //Salva as alterações do contexto
                _context.SaveChanges();

                return usuario;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Remover(Guid id)
        {
            try
            {
                var usuario = BuscarPorId(id);
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Usuario Editar(Usuario usuario)
        {
            try
            {
                //Buscar o usuario no Banco de Dados
                var usuarioexiste = BuscarPorId(usuario.Id);
                //Verifica se o usuário foi encontrado
                if (usuarioexiste == null)
                    throw new Exception("Usuário não encontrado");
                //Altera os valores do usuário
                usuarioexiste.Nome = usuario.Nome;
                usuarioexiste.Email = usuario.Email;
                if (!string.IsNullOrEmpty(usuario.Senha))
                    usuarioexiste.Senha = usuario.Senha;

                _context.Usuarios.Update(usuarioexiste);
                _context.SaveChanges();
                return usuarioexiste;


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Usuario Logar(string email, string senha)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(c => c.Email == email && c.Senha == senha);
                // c = usuarios
                return usuario;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
