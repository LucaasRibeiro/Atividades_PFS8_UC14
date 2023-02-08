using ApiFS8Livros.Contexts;
using ApiFS8Livros.Models;

namespace ApiFS8Livros.Repositories
{
    public class LivroRepository
    {
        private readonly SqlContext _context;
        public LivroRepository(SqlContext context) //injeção de dependência / ligando o Context
        {
            _context = context;
        }


        public List<Livro> Listar()
        {
            return _context.Livros.ToList();//listando os dados da tabela livros
        }
    }
}
