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
            throw new NotImplementedException();
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

        public void Deletar(Guid id)
        {
            throw new NotImplementedException();
        }

        public Usuario Editar(Usuario usuario)
        {
            throw new NotImplementedException();
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
