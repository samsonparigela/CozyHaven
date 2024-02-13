using CozyHaven.Context;
using CozyHaven.Interfaces;
using CozyHaven.Models;
using CozyHaven.Models.DTOs;
using CozyHaven.Repository;
using Microsoft.EntityFrameworkCore;

namespace CozyHaven.Services
{
    public class HotelService : IHotelAdminService, IHotelCustomerService, IHotelOwnerService
    {
        private readonly IRepository<int, Hotel> _repo;
        private readonly ILogger<HotelService> _logger;

        public HotelService(IRepository<int, Hotel> repo, ILogger<HotelService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Hotel> AddHotel(Hotel hotel)
        {
            hotel = await _repo.Add(hotel);
            return hotel;
        }

        public async Task<Hotel> DeleteHotel(int hotelId)
        {
            var hotel = await _repo.Delete(hotelId);
            return hotel;
        }

        public async Task<Hotel> GetHotel(int hotelId)
        {
            var hotel = await _repo.Get(hotelId);
            return hotel;
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            var hotels = await _repo.GetAll();
            return hotels;
        }

        public async Task<HotelDTO> UpdateHotel(HotelDTO hotelDTO)
        {
            var hotel = await _repo.Get(hotelDTO.Id);
            if (hotel != null)
            {
                hotel.Name = hotelDTO.Name;
                hotel.Location = hotelDTO.Location;
                hotel = await _repo.Update(hotel);
                return new HotelDTO
                {
                    Id = hotel.HotelID,
                    Name = hotel.Name,
                    Location = hotel.Location
                };
            }
            throw new HotelNotFoundException();
        }

        public async Task<List<Hotel>> GetAllHotelsByOwner(int ownerId)
        {
            var hotels = await _repo.GetAll();
            return hotels.Where(h => h.HotelOwner.HotelOwnerID == ownerId).ToList();
        }
    }
}