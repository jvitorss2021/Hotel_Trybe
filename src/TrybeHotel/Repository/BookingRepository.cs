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
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var room = _context.Rooms.FirstOrDefault(r => r.RoomId == booking.RoomId);
        if (room == null)
        {
            throw new Exception("Room not found");
        }
        if (booking.GuestQuant > room.Capacity)
        {
            throw new Exception("Guest quantity over room capacity");
        }

        var newBooking = new Booking
        {
            CheckIn = booking.CheckIn,
            CheckOut = booking.CheckOut,
            UserId = user.UserId,
            GuestQuant = booking.GuestQuant,
            RoomId = booking.RoomId
        };

        _context.Bookings.Add(newBooking);
        _context.SaveChanges();
            var hotel = _context.Hotels
                        .Join(_context.Cities, h => h.CityId, c => c.CityId, (h, c) => new { h, c })
                        .Where(hc => hc.h.HotelId == room.HotelId)
                        .Select(hc => new HotelDto
                        {
                            HotelId = hc.h.HotelId,
                            Name = hc.h.Name,
                            Address = hc.h.Address,
                            CityId = hc.h.CityId,
                            CityName = hc.c.Name,
                        })
                        .FirstOrDefault();

        return new BookingResponse
        {
            BookingId = newBooking.BookingId,
            CheckIn = newBooking.CheckIn,
            CheckOut = newBooking.CheckOut,
            GuestQuant = newBooking.GuestQuant,
            User = user,
            Room = new RoomDto
            {
                RoomId = room.RoomId,
                Name = room.Name,
                Capacity = room.Capacity,
                Image = room.Image,
                Hotel = hotel
            }
        };
    }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId && b.UserId == user.UserId);
            if (booking == null)
            {
                return null!;
            }

            var room = _context.Rooms
                        .Join(_context.Hotels, r => r.HotelId, h => h.HotelId, (r, h) => new { r, h })
                        .Join(_context.Cities, rh => rh.h.CityId, c => c.CityId, (rh, c) => new { rh, c })
                        .Where(rhc => rhc.rh.r.RoomId == booking.RoomId)
                        .Select(rhc => new RoomDto
                        {
                            RoomId = rhc.rh.r.RoomId,
                            Name = rhc.rh.r.Name,
                            Capacity = rhc.rh.r.Capacity,
                            Image = rhc.rh.r.Image,
                            Hotel = new HotelDto
                            {
                                HotelId = rhc.rh.h.HotelId,
                                Name = rhc.rh.h.Name,
                                Address = rhc.rh.h.Address,
                                CityId = rhc.rh.h.CityId,
                                CityName = rhc.c.Name,
                            }
                        })
                        .FirstOrDefault();

            return new BookingResponse
            {
                BookingId = booking.BookingId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                User = user,
                Room = room
            };
        }

        public Room GetRoomById(int RoomId)
        {
            throw new NotImplementedException();
        }

    }

}