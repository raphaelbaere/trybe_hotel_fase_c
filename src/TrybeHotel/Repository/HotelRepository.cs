using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Desenvolva o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {
            var allHotels = _context.Hotels.Select(hotel => new HotelDto
            {
               HotelId = hotel.HotelId,
               CityId = hotel.CityId,
               Name = hotel.Name,
               Address = hotel.Address,
               CityName = _context.Cities.First(city => city.CityId == hotel.CityId).Name
            }).ToList();
            return allHotels;
        }
        
        // 5. Desenvolva o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
            return new HotelDto
            {
               HotelId = hotel.HotelId,
               CityId = hotel.CityId,
               Name = hotel.Name,
               Address = hotel.Address,
               CityName = _context.Cities.First(city => city.CityId == hotel.CityId).Name
            };
        }
    }
}