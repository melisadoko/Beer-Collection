using AutoMapper;
using BeerCollectionBLL.DTO;
using BeerCollectionBLL.IServices;
using BeerCollectionDAL.Entities;
using BeerCollectionDAL.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCollectionBLL.Services
{
    public class BeerService : IBeerService
    {
        public readonly IBeerRepository _beerRepository;
        public readonly IMapper _mapper;
        private readonly ILogger<BeerService> _logger;
        public BeerService(IBeerRepository beerRepository, IMapper mapper, ILogger<BeerService> logger)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddBearAsync(BeerCreatedDto beerDto)
        {
            try
            {
                if (beerDto.Rating < 1 || beerDto.Rating > 5)
                    throw new ArgumentException("Rating must be between 1 and 5");

                Beer beer = _mapper.Map<Beer>(beerDto);
                await _beerRepository.AddBeerAsync(beer);

                _logger.LogInformation("Beer was saved successfully");
                return beer.Id;
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Validation failed: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred while adding a beer.");
                throw;
            }
        }
 
        public async Task<BeerDto> GetBeerByIdAsync(int id)
        {
            try
            {
                Beer beer = await _beerRepository.GetBeerByIdAsync(id);
                var beerDto = _mapper.Map<BeerDto>(beer);
                _logger.LogInformation($"Beer with id: {id} was fetched successfully");
                return beerDto;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred while retrieving the beer with ID {Id}.", id);
                throw;
            }
        }
        public async Task<List<BeerDto>> GetAllBeersAsync()
        {
            try
            {
                var beers = await _beerRepository.GetAllBeersAsync();

                var beerDtos = _mapper.Map<List<BeerDto>>(beers);
                _logger.LogInformation($"List of beers was fetched successfully");
                return beerDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the list of beers.");
                throw;
            }
        }

        public async Task<List<BeerDto>> SearchBeersByNameAsync(string name)
        {
            try
            {
                var beers = await _beerRepository.SearchBeersByNameAsync(name);

                var beerDtos = _mapper.Map<List<BeerDto>>(beers);
                _logger.LogInformation($"List of beers which contain name: {name} was fetched successfully");
                return beerDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for beers.");
                throw;
            }
        }

        public async Task UpdateBeerRatingAsync(int id, decimal rating)
        {
            try
            {
                Beer beer = await _beerRepository.GetBeerByIdAsync(id);

                if (beer == null)
                    throw new Exception("Beer was not found.");

                if (rating < 1 || rating > 5)
                    throw new ArgumentException("Rating must be between 1 and 5");

                beer.Rating = !beer.Rating.HasValue ? rating : (beer.Rating.Value + rating) / 2;

                await _beerRepository.UpdateBeerAsync(beer);
                _logger.LogInformation($"Rating for beer with id {id} updated successfully.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Validation failed: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a beer.");
                throw;
            }
        }
    }
}
