using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 6. Desenvolva o endpoint GET /room/:hotelId
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            var specificHotel = _context.Hotels.First(hotel => hotel.HotelId == HotelId);
            var rooms = _context.Rooms.Where(room => room.HotelId == HotelId).Select(room => new RoomDto
            {
                RoomId = room.RoomId,
                Name = room.Name,
                Capacity = room.Capacity,
                Image = room.Image,
                Hotel = new HotelDto
                {
                    HotelId = specificHotel.HotelId,
                    Name = specificHotel.Name,
                    Address = specificHotel.Address,
                    CityId = specificHotel.CityId,
                    CityName = _context.Cities.First(city => city.CityId == specificHotel.CityId).Name
                }
            }).ToList();
            return rooms;
        }
        // 7. Desenvolva o endpoint POST /room
        public RoomDto AddRoom(Room room) {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            var newRoom = new RoomDto
            {
                RoomId = room.RoomId,
                Capacity = room.Capacity,
                Name = room.Name,
                Image = room.Image,
                Hotel = _context.Hotels.Where(hotel => hotel.HotelId == room.HotelId).Select(hotel => new HotelDto
                {
                    HotelId = hotel.HotelId,
                    CityId = hotel.CityId,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    CityName = _context.Cities.First(city => city.CityId == hotel.CityId).Name
                }).FirstOrDefault()
            };
            return newRoom;
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        public void DeleteRoom(int RoomId) {
            _context.Rooms.Remove(_context.Rooms.First(room => room.RoomId == RoomId));
            _context.SaveChanges();
        }
    }
}