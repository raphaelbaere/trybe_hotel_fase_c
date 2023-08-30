using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Refatore o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {
            var allCities = _context.Cities.Select(city => new CityDto
            {
                CityId = city.CityId,
                Name = city.Name,
                State = city.State,
            }).ToList();
            return allCities;
        }

        // 3. Desenvolva o endpoint POST /city
        public CityDto AddCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
            return new CityDto
            {
                CityId = city.CityId,
                Name = city.Name,
                State = city.State,
            };
        }

        // 3. Desenvolva o endpoint PUT /city
        public CityDto UpdateCity(City city)
        {
           var cityToBeUpdated = _context.Cities.FirstOrDefault(cityEl => cityEl == city);
           cityToBeUpdated.Name = city.Name;
           cityToBeUpdated.State = city.State;
           _context.SaveChanges();
           return new CityDto
           {
            CityId = cityToBeUpdated.CityId,
            Name = cityToBeUpdated.Name,
            State = cityToBeUpdated.State,
           };
        }

    }
}