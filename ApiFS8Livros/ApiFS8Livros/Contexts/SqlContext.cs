using ApiFS8Livros.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFS8Livros.Contexts
{
    public class SqlContext : DbContext
    {
        public SqlContext() { }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

        // vamos utilizar esse método para configurar o banco de dados
        protected override void
        OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // cada provedor tem sua sintaxe para especificação
                //optionsBuilder.UseSqlServer("Data Source = BENIGNO\\SQLEXPRESS; initial catalog = Chapter; user id = sa; password = 12535");
                optionsBuilder.UseSqlServer("Data Source = BENIGNO\\SQLEXPRESS; initial catalog = Chapter; user id = sa; password = 121535");
            }
        }
        // dbset representa as entidades que serão utilizadas nas operações de leitura, c riação, atualização e deleção
        public DbSet<Livro> Livros { get; set; }


    }
}
