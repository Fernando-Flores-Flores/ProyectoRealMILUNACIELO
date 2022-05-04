using BackMiLunaCielo.Models;

namespace BackMiLunaCielo.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        //Aqui se guardan los metodos del modelo , en este caso Pelicula
        ICollection<Usuario> GetUsuarios();

        Usuario GetUsuario(int UsuarioId);

        bool ExisteUsuario(string usuario);

        Usuario Registro(Usuario usuario, string password);

        Usuario Login(string usuario, string password);

        bool Guardar();

    }
}
