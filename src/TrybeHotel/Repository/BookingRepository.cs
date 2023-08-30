using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {    
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == booking.RoomId);
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == room.HotelId);
            var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel.CityId);
            if (room == null || booking.GuestQuant > room.Capacity)
            {
                return null;
            }
            var bookingToAdd = new Booking
            {
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                UserId = user.UserId,
                Room = room,
            };
            _context.Bookings.Add(bookingToAdd);
            _context.SaveChanges();
            return new BookingResponse
            {
                BookingId = bookingToAdd.BookingId,
                CheckIn = bookingToAdd.CheckIn,
                CheckOut = bookingToAdd.CheckOut,
                GuestQuant = bookingToAdd.GuestQuant,
                Room = new RoomDto {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto {
                        HotelId = hotel!.HotelId,
                        Name = hotel.Name,
                        Address = hotel.Address,
                        CityId = hotel.CityId,
                        CityName = city.Name,
                    }
                },

            };
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (booking == null || booking.UserId != user.UserId)
            {
                return null;
            }
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == booking.RoomId);
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == room.HotelId);
            var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel.CityId);
        return new BookingResponse
            {
                BookingId = bookingId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                Room = new RoomDto {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto {
                        HotelId = hotel!.HotelId,
                        Name = hotel.Name,
                        Address = hotel.Address,
                        CityId = hotel.CityId,
                        CityName = city.Name,
                    }
                },

            };
        }

        public Room GetRoomById(int RoomId)
        {
            throw new NotImplementedException();
        }

    }

}