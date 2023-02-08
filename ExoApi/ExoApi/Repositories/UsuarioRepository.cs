using ExoApi.Contexts;
using ExoApi.Interfaces;
using ExoApi.Models;

namespace ExoApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SqlContext _exoapicontext;
        public UsuarioRepository(SqlContext context)// injeção de dependência/ligando o Context
        {
            _exoapicontext = context;
        }


        public void Atualizar(int id, Usuario usuario)
        {
            Usuario usuarioBuscado = _exoapicontext.Usuarios.Find(id);

            if (usuarioBuscado != null)
            {
                usuarioBuscado.Email = usuario.Email;
                usuarioBuscado.Senha = usuario.Senha;
                usuarioBuscado.Tipo = usuario.Tipo;

                _exoapicontext.Usuarios.Update(usuarioBuscado);
                _exoapicontext.SaveChanges();
            }
        }

        public Usuario BuscarPorId(int id)
        {
            return _exoapicontext.Usuarios.Find(id);
        }

        public void Cadastrar(Usuario usuario)
        {
            _exoapicontext.Usuarios.Add(usuario);
            _exoapicontext.SaveChanges();
        }

        public void Deletar(int id)
        {
            Usuario usuarioBuscado = _exoapicontext.Usuarios.Find(id);
            _exoapicontext.Usuarios.Remove(usuarioBuscado);
            _exoapicontext.SaveChanges();
        }


        public List<Usuario> Listar()
        {
            return _exoapicontext.Usuarios.ToList();
        }

        public Usuario Login(string email, string senha)
        {
            throw new NotImplementedException();
        }
    }
}
