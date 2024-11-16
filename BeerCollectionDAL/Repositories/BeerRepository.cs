using BeerCollectionDAL.Entities;
using BeerCollectionDAL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BeerCollectionDAL.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly AppDbContext _context;

        public BeerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddBeerAsync(Beer beer)
        {
            try
            {
                await _context.Beers.AddAsync(beer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving the beer to the database. {ex.Message} ");
            }
        }

        public async Task<Beer> GetBeerByIdAsync(int id)
        {
            try
            {
                return await _context.Beers.FirstOrDefaultAsync(beer => beer.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the beer from the database.", ex);
            }
        }
        public async Task<List<Beer>> GetAllBeersAsync()
        {

            try
            {
                return await _context.Beers.ToListAsync(); 
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the beers from the database.", ex);
            }
        }

        public async Task<List<Beer>> SearchBeersByNameAsync(string name)
        {
            try
            {
                return await _context.Beers
                    .Where(b => b.Name.ToLower().Contains(name.ToLower()))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while searching for beers.", ex);
            }
        }

        public async Task UpdateBeerAsync(Beer beer)
        {
            try
            {
                _context.Beers.Update(beer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the beer rating in the database.", ex);
            }
        }
    }
}
