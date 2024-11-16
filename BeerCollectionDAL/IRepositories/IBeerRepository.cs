using BeerCollectionDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCollectionDAL.IRepositories
{
    public interface IBeerRepository
    {
        public Task AddBeerAsync(Beer beer);
        public Task<Beer> GetBeerByIdAsync(int id);
        public Task<List<Beer>> GetAllBeersAsync();
        public Task<List<Beer>> SearchBeersByNameAsync(string name);
        public Task UpdateBeerAsync(Beer beer);

    }
}
