using BackMiLunaCielo.Data;
using BackMiLunaCielo.Models;
using BackMiLunaCielo.Repository.IRepository;

namespace BackMiLunaCielo.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly CategoriaDbContext _bd;

        public CategoriaRepository(CategoriaDbContext bd)
        {
            _bd = bd;

        }

        public bool ActualizarCategoria(Categoria categoria)
        {
            _bd.Categoria.Update(categoria);
            return Guardar();
        }

        public bool BorrarCategoria(Categoria categoria)
        {
            _bd.Categoria.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(Categoria categoria)
        {
            _bd.Categoria.Add(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(string nombre)
        {
            bool valor = _bd.Categoria.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteCategoria(int id)
        {
            return _bd.Categoria.Any(c => c.Id == id);

        }

        public Categoria GetCategoria(int CategoriaId)
        {
            return _bd.Categoria.FirstOrDefault(c => c.Id == CategoriaId);
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _bd.Categoria.OrderBy(c => c.Id).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    
}
}
