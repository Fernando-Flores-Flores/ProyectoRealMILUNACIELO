using BackMiLunaCielo.Data;
using BackMiLunaCielo.Models;
using BackMiLunaCielo.Repository.IRepository;

namespace BackMiLunaCielo.Repository
{
    public class UsuarioRepository:IUsuarioRepository
    {
        private readonly CategoriaDbContext _bd;

        public UsuarioRepository(CategoriaDbContext bd)
        {
            _bd = bd;
        }

        public bool ExisteUsuario(string usuario)
        {
            if (_bd.Usuario.Any(x=>x.UsuarioA == usuario))
            {
                return true;
            }
            return false;
        }

        public Usuario GetUsuario(int UsuarioId)
        {
            return _bd.Usuario.FirstOrDefault(c => c.Id == UsuarioId);
        }


        public ICollection<Usuario> GetUsuarios()
        {
            return _bd.Usuario.OrderBy(c => c.UsuarioA).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >=0 ? true :false;
        }

        public Usuario Login(string usuario, string password)
        {
            throw new NotImplementedException();
        }

        public Usuario Registro(Usuario usuario, string password)
        {
            throw new NotImplementedException();
        }
    }
}
