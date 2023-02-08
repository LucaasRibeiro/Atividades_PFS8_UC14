using ExoApi.Models;

namespace ExoApi.Interfaces
{
    public interface IProjetoRepository
    {
        List<Projeto> Ler();

        void Cadastrar(Projeto projeto);

        void Atualizar(int id, Projeto projeto);

        void Deletar(int id);

        Projeto BuscarPorId(int id);

        Projeto BuscarPorTitulo(string titulo);
    }
}
