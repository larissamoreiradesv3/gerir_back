using Senai.Gerir.back.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.back.Interfaces
{
    interface IUsuarioRepositorio
    {

        /// <summary>
        /// Cadastra o usuário
        /// </summary>
        /// <param name="usuario">informações do usuário</param>
        /// <returns></returns>
        Usuario Cadastrar(Usuario usuario);

        /// <summary>
        /// Logar conta do Usuário
        /// </summary>
        /// <param name="email">email do usuário</param>
        /// <param name="senha">senha do usuário</param>
        /// <returns></returns>
        Usuario Logar(string email, string senha);

        /// <summary>
        /// Editar informações do usuário
        /// </summary>
        /// <param name="usuario">pega informações do usuário</param>
        /// <returns></returns>
        Usuario Editar(Usuario usuario);

        /// <summary>
        /// Deletar informações do Usuário
        /// </summary>
        /// <param name="id">Id do Usuário</param>
        void Remover(Guid id);

        /// <summary>
        /// Busca o usuário pelo Id
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns></returns>
        Usuario BuscarPorId(Guid id);
    }
}
