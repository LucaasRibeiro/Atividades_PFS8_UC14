using ExoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExoApi.Contexts
{
    public class SqlContext : DbContext
    {

        public SqlContext()
        {
        }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        // vamos utilizar esse método para configurar o banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // cada provedor tem sua sintaxe para especificação
                optionsBuilder.UseSqlServer("Data Source = BENIGNO\\SQLEXPRESS; initial catalog = Exoapi; user id = sa; password = 121535");
            }
        }
        // dbset representa as entidades que serão utilizadas nas operações de leitura, criação, atualização e deleção
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


    }
}
