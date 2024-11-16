using BeerCollectionBLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCollectionBLL.IServices
{
    public interface IBeerService
    {
        public Task<int> AddBearAsync(BeerCreatedDto beerDto);
        public Task<BeerDto> GetBeerByIdAsync(int id);
        public Task<List<BeerDto>> GetAllBeersAsync();
        public Task<List<BeerDto>> SearchBeersByNameAsync(string name);
        public Task UpdateBeerRatingAsync(int id, decimal rating);
    }
}
