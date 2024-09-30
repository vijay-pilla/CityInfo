using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    [Route("api/cities")]
    //[Consumes("application/json")]

    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPazeSize = 20;

        //private readonly CitiesDataStore _citiesDataStore;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities(string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10 )
        {
            if(pageSize > maxCitiesPazeSize)
            {
                pageSize = maxCitiesPazeSize;
            }
            var (response, paginationMetadata) = await _cityInfoRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            var result = _mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(response);
            //foreach (var city in response) {
            //    result.Add(new CityWithoutPointOfInterestDto
            //    {
            //        Id = city.Id,
            //        Name = city.Name,
            //        Description = city.Description
            //    });
            //    }
            
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool includePointOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointOfInterest && city.PointOfInterest != null && city.PointOfInterest.Any())
            {
                // Map city including points of interest
                var cityWithPointsOfInterest = _mapper.Map<CityDto>(city);
                return Ok(cityWithPointsOfInterest);
            }

            // Map city without points of interest
            var cityWithoutPointsOfInterest = _mapper.Map<CityWithoutPointOfInterestDto>(city);
            return Ok(cityWithoutPointsOfInterest);
        }


    }
}
