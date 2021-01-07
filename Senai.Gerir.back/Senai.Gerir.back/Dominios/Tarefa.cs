﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Senai.Gerir.back.Dominios
{
    public partial class Tarefa
    {
        public Tarefa()
        {
            Id = Guid.NewGuid();
            Status = false;
        }
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public DateTime DataEntrega { get; set; }
        public bool? Status { get; set; }
        public Guid? UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
