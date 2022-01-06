﻿using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private Context _context;

        public ClienteRepository(Context context)
        {
            _context = context;
        }

        public void Create(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            var cliente = Get(id);
            _context.Clientes.Remove(cliente);
            SaveChanges();
        }

        public Cliente Get(Guid id)
        {
            return _context.Clientes.Find(id);
        }

        public List<Cliente> GetAll()
        {
            return _context.Clientes.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));
            var entry = _context.Clientes.Find(cliente.CUIT);
            entry.Domicilio = cliente.Domicilio;
            entry.RazonSocial = cliente.RazonSocial;
            SaveChanges();
        }
    }
}
