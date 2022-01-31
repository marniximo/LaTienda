using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaTienda.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private Context _context;

        public UsuarioRepository(Context context)
        {
            _context = context;
        }

        public void Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var usuario = Get(id);
            _context.Usuarios.Remove(usuario);
            SaveChanges();
        }

        public Usuario Get(Guid id)
        {
            return _context.Usuarios.Find(id);
        }

        public List<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));
            var entry = _context.Usuarios.Find(usuario.Legajo);
            SaveChanges();
        }
    }
}
