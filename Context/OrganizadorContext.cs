using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_netapi.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_netapi.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {
        }

        public DbSet<TarefaModel> Tarefas { get; set; }
    }
}