using System.Net.Http;
using System.Threading.Tasks;
using TrybeHotel.Dto;
using TrybeHotel.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace TrybeHotel.Services
{
    public class GeoService : IGeoService
    {
        private readonly HttpClient _client;

        public GeoService(HttpClient client)
        {
            _client = client;
        }

        // 11. Desenvolva o endpoint GET /geo/status
        public async Task<object> GetGeoStatus()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://nominatim.openstreetmap.org/status.php?format=json");
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("User-Agent", "aspnet-user-agent");

            var response = await _client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<object>(content);
            }

            return default(object);
        }

        // 12. Desenvolva o endpoint GET /geo/address
        public async Task<GeoDtoResponse> GetGeoLocation(GeoDto geoDto)
        {
            var address = $"{geoDto.Address}, {geoDto.City}, {geoDto.State}, Brazil";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://nominatim.openstreetmap.org/search?street={geoDto.Address}&city={geoDto.City}&state={geoDto.State}&country=Brazil&format=json&limit=1");
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("User-Agent", "aspnet-user-agent");

            var response = await _client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var locations = JsonConvert.DeserializeObject<List<GeoDtoResponse>>(content);
                return locations?.FirstOrDefault() ?? default(GeoDtoResponse);
            }

            return default(GeoDtoResponse);
        }

        // 12. Desenvolva o endpoint GET /geo/address
        public async Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository)
        {
            var geoLocation = await GetGeoLocation(geoDto);
            if (geoLocation == null)
            {
                return new List<GeoDtoHotelResponse>();
            }

            var hotels = repository.GetHotels();
            var hotelResponses = new List<GeoDtoHotelResponse>();

            foreach (var hotel in hotels)
            {

            var hotelCoordinates = await GetGeoLocation(new GeoDto
                {
                    Address = hotel.Address,
                    City = hotel.CityName,
                    State = hotel.State
                });

                if (hotelCoordinates != null)
                {
                    int distance = CalculateDistance(
                        geoLocation.Lat,
                        geoLocation.Lon,
                        hotelCoordinates.Lat,
                        hotelCoordinates.Lon
                    );

                    hotelResponses.Add(new GeoDtoHotelResponse
                    {
                        HotelId = hotel.HotelId,
                        Name = hotel.Name,
                        Address = hotel.Address,
                        CityName = hotel.CityName,
                        State = hotel.State,
                        Distance = distance
                    });
                }
            }

        return hotelResponses.OrderBy(h => h.Distance).ToList();
        }

        public int CalculateDistance (string latitudeOrigin, string longitudeOrigin, string latitudeDestiny, string longitudeDestiny) {
            double latOrigin = double.Parse(latitudeOrigin.Replace('.',','));
            double lonOrigin = double.Parse(longitudeOrigin.Replace('.',','));
            double latDestiny = double.Parse(latitudeDestiny.Replace('.',','));
            double lonDestiny = double.Parse(longitudeDestiny.Replace('.',','));
            double R = 6371;
            double dLat = radiano(latDestiny - latOrigin);
            double dLon = radiano(lonDestiny - lonOrigin);
            double a = Math.Sin(dLat/2) * Math.Sin(dLat/2) + Math.Cos(radiano(latOrigin)) * Math.Cos(radiano(latDestiny)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double distance = R * c;
            return int.Parse(Math.Round(distance,0).ToString());
        }

        public double radiano(double degree)
        {
            return degree * Math.PI / 180;
        }
    }
}