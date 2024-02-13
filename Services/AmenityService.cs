using CozyHaven.Context;
using CozyHaven.Exceptions;
using CozyHaven.Interfaces;
using CozyHaven.Models;
using CozyHaven.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CozyHaven.Services
{
    public class AmenityService : IAmenityAdminService, IAmenityCustomerService, IAmenityOwnerService
    {
        private readonly IRepository<int, Amenity> _repo;
        private readonly ILogger<AmenityService> _logger;

        public AmenityService(IRepository<int, Amenity> repo, ILogger<AmenityService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Amenity> AddAmenity(Amenity amenity)
        {
            amenity = await _repo.Add(amenity);
            return amenity;
        }

        public async Task<Amenity> DeleteAmenity(int amenityId)
        {
            var amenity = await _repo.Delete(amenityId);
            return amenity;
        }

        public async Task<Amenity> GetAmenity(int amenityId)
        {
            var amenity = await _repo.Get(amenityId);
            return amenity;
        }

        public async Task<List<Amenity>> GetAllAmenities()
        {
            var amenities = await _repo.GetAll();
            return amenities;
        }

        public async Task<AmenityDTO> UpdateAmenity(AmenityDTO amenityDTO)
        {
            var amenity = await _repo.Get(amenityDTO.Id);
            if (amenity != null)
            {
                amenity.Name = amenityDTO.Name;
                amenity.Description = amenityDTO.Description;
                amenity = await _repo.Update(amenity);
                return new AmenityDTO
                {
                    Id = amenity.AmenityID,
                    Name = amenity.Name,
                    Description = amenity.Description
                };
            }
            throw new AmenityNotFoundException();
        }

        //public async Task<List<Amenity>> GetAllAmenitiesByOwner(int ownerId)
        //{
        //    var amenities = await _repo.GetAll();
        //    return amenities.Where(a => a.HotelAmenities.Any(ha => ha.Hotel.HotelOwnerID == ownerId)).ToList();
        //}
    }
}