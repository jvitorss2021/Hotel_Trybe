namespace TrybeHotel.Dto {
     public class HotelDto {
        public int HotelId { get; set; }
        public string ?Name { get; set; }
        public string ?Address { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public string? State { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public ICollection<RoomDto>? Rooms { get; set; }

    }
}