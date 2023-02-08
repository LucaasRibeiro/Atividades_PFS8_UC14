using ExoApi.Contexts;
using ExoApi.Interfaces;
using ExoApi.Models;

namespace ExoApi.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly SqlContext _exoapicontext;
        public ProjetoRepository(SqlContext context)// injeção de dependência/ligando o Context
        {
            _exoapicontext = context;
        }

        public void Atualizar(int id, Projeto projeto)
        {
            Projeto projetoBuscado = _exoapicontext.Projetos.Find(id);
            if (projetoBuscado != null)
            {
                projetoBuscado.Titulo = projeto.Titulo;
                projetoBuscado.Statos = projeto.Statos;
                projetoBuscado.Tecnologias = projeto.Tecnologias;
                projetoBuscado.Requisitos = projeto.Requisitos;
                projetoBuscado.Area = projeto.Area;
                projetoBuscado.DataInicio = projeto.DataInicio;
            }

            _exoapicontext.Projetos.Update(projetoBuscado);
            _exoapicontext.SaveChanges();
        }

        public Projeto BuscarPorId(int id)
        {
            return _exoapicontext.Projetos.Find(id);
        }

        public Projeto BuscarPorTitulo(string titulo)
        {
            return _exoapicontext.Projetos.FirstOrDefault(t => t.Titulo == titulo.Trim());
        }

        public void Cadastrar(Projeto projeto)
        {
            _exoapicontext.Projetos.Add(projeto);
            _exoapicontext.SaveChanges();
        }

        public void Deletar(int id)
        {
            Projeto projeto = _exoapicontext.Projetos.Find(id);
            _exoapicontext.Projetos.Remove(projeto);
            _exoapicontext.SaveChanges();
        }

        public List<Projeto> Ler()
        {
            return _exoapicontext.Projetos.ToList();//Listando dados da tabela Projetos
        }
             
    }
}
