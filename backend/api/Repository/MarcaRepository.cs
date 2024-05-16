using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;

namespace api.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        public Task<Marca> CreateAsync(Marca marcaModel)
        {
            throw new NotImplementedException();
        }

        public Task<Marca?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Marca>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Marca?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarcaExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Marca?> UpdateAsync(int id, Marca marcaModel)
        {
            throw new NotImplementedException();
        }
    }
}