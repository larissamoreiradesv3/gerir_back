using Senai.Gerir.back.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.back.Interfaces
{
    interface ITarefaRepositorio
    {
        /// <summary>
        /// Cadastra a tarefa 
        /// </summary>
        /// <param name="tarefa">Informações da tarefa</param>
        /// <returns></returns>
        Tarefa Cadastrar(Tarefa tarefa);

        /// <summary>
        /// Visualizar as tarefas
        /// </summary>
        /// <param name="IdUsuario">Para listar todas as tarefas do usuário </param>
        /// <returns></returns>
        List<Tarefa> ListarTodos(Guid IdUsuario);

        /// <summary>
        /// Fazer alterações na tarefa
        /// </summary>
        /// <param name="IdTarefa">Para alterar apenaso status de uma única tabela em quetão</param>
        /// <returns></returns>
        Tarefa AlterarStatus(Guid IdTarefa);

        /// <summary>
        /// Para remover uma tarefa
        /// </summary>
        /// <param name="IdTarefa">Para remover apenas a única tarefa em questão</param>
        /// <returns></returns>
        void Remover(Guid IdTarefa);

        /// <summary>
        /// Para editar uma tarefa
        /// </summary>
        /// <param name="tarefa">Para editar a informação da tabela</param>
        /// <returns></returns>
        Tarefa Editar(Tarefa tarefa);

        /// <summary>
        /// Para buscar uma tarefa
        /// </summary>
        /// <param name="IdTarefa">utilza o Id para encontrar apenas uma tarefa específica</param>
        /// <returns></returns>
        Tarefa BuscarPorId(Guid IdTarefa);
    }
}
