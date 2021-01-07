using Senai.Gerir.back.Contextos;
using Senai.Gerir.back.Dominios;
using Senai.Gerir.back.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.back.Repositorio
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly GerirContext _context;

        public TarefaRepositorio()
        {
            _context = new GerirContext();
        }
        public Tarefa Editar(Tarefa tarefa)
        {
            try
            {
                //Buscar a tarefa no Banco de Dados
                var tarefaexistente = BuscarPorId(tarefa.Id);
                //Verifica se a tarefa foi encontrado
                if (tarefaexistente == null)
                    throw new Exception("Tarefa não encontrado");
                //Altera os valores da tarefa
                tarefaexistente.Titulo = tarefaexistente.Titulo;
                tarefaexistente.Descricao =tarefaexistente.Descricao;
                if (!string.IsNullOrEmpty(tarefa.Titulo))
                    tarefaexistente.Titulo = tarefa.Titulo; ;

                _context.Tarefas.Update(tarefaexistente);
                _context.SaveChanges();
                return tarefaexistente;


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Tarefa AlterarStatus(Guid IdTarefa)
        {
            try
            {
                //Buscar a terefa pelo Id
                var tarefasolicitada = BuscarPorId(IdTarefa);

                //Se a tarefa solicitada estiver 0 ela muda para 1, e estiver 1 ela muda para 0
                if(tarefasolicitada.Status == false)
                {
                    tarefasolicitada.Status = true;
                }
                else
                {
                    tarefasolicitada.Status = false;
                }

                _context.Tarefas.Update(tarefasolicitada);
                _context.SaveChanges();
                return tarefasolicitada;


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Tarefa BuscarPorId(Guid IdTarefa)
        {
            try
            {
                //para buscar o id da tarefa usando o metodo Find 
                var tarefa = _context.Tarefas.Find(IdTarefa); //Tarefas é referente ao Banco de Dados
                //após buscar retorna para o tarefa 
                return tarefa;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Tarefa Cadastrar(Tarefa tarefa)
        {
            try
            {
                //adiciona uma tarefa ao DbSet Tarefa do contexto
                _context.Tarefas.Add(tarefa);
                //Salva as alterações do contexto
                _context.SaveChanges();

                return tarefa;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public List<Tarefa> ListarTodos(Guid IdUsuario)
        {
            //Buscar a tarefa somente do usuário
            var listatarefas = _context.Tarefas.Where(c => c.UsuarioId == IdUsuario).ToList();

            return listatarefas; //conversão para list 
        }

        public void Remover(Guid IdTarefa)
        {
            try
            {
                var tarefa = BuscarPorId(IdTarefa);
                _context.Tarefas.Remove(tarefa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
