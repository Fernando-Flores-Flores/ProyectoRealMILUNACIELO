using BackMiLunaCielo.Models;

namespace BackMiLunaCielo.Repository.IRepository
{
    public interface ICategoriaRepository
    {
        //Aqui se guardan los metodos del modelo , en este caso categoria
        ICollection<Categoria> GetCategorias();

        Categoria GetCategoria(int CategoriaId);

        bool ExisteCategoria(string nombre);

        bool ExisteCategoria(int id);

        bool CrearCategoria(Categoria categoria);

        bool ActualizarCategoria(Categoria categoria);

        bool BorrarCategoria(Categoria categoria);

        bool Guardar();
    }
}
