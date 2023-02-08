using Chapter.Models;

namespace Chapter.Interfaces
{
    /// <summary>
    /// Interface ILivroRepository
    /// </summary>
    public interface ILivroRepository
    {
        List<Livro> Ler();//método que deverá ser implementado pela classe que herdar desta interface

        void Cadastrar(Livro livro);

        void Atualizar(int id, Livro livro);

        void Deletar(int id);

        Livro BuscarPorId(int id);

        Livro BuscarPorTitulo(string titulo);
    }
}
