using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

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
            return _context.Rooms
                .Include(r => r.Hotel)
                .Where(r => r.HotelId == HotelId)
                .Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    HotelId = r.HotelId,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Image = r.Image,
                    Hotel = r.Hotel != null ? new HotelDto{
                        HotelId = r.Hotel.HotelId,
                        Name = r.Hotel.Name,
                        Address = r.Hotel.Address,
                        CityId = r.Hotel.CityId,
                        CityName = r.Hotel.City != null ? r.Hotel.City.Name : null,
                        State = r.Hotel.City != null ? r.Hotel.City.State : null,
                    } : null,
                }).ToList();
        }

        // 7. Desenvolva o endpoint POST /room
        public RoomDto AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();

            var addedRoom = _context.Rooms
                .Include(r => r.Hotel)
                .ThenInclude(h => h.City)
                .First(r => r.RoomId == room.RoomId);

            return new RoomDto
            {
                RoomId = addedRoom.RoomId,
                HotelId = addedRoom.HotelId,
                Name = addedRoom.Name,
                Capacity = addedRoom.Capacity,
                Image = addedRoom.Image,
                Hotel = addedRoom.Hotel != null ? new HotelDto
                {
                    HotelId = addedRoom.Hotel.HotelId,
                    Name = addedRoom.Hotel.Name,
                    Address = addedRoom.Hotel.Address,
                    CityId = addedRoom.Hotel.CityId,
                    CityName = addedRoom.Hotel.City?.Name,
                    State = addedRoom.Hotel.City != null ? addedRoom.Hotel.City.State : null,
                } : null,
            };
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        public void DeleteRoom(int RoomId) {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == RoomId);
            if (room != null) {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
        }
    }
}