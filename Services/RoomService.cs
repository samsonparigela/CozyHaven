using CozyHaven.Exceptions;
using CozyHaven.Interfaces;
using CozyHaven.Models;
using CozyHaven.Models.DTOs;

namespace CozyHaven.Services
{
    public class RoomService : IRoomAdminService, IRoomCustomerService, IRoomHotelOwnerService
    {
        private readonly IRepository<int, Room> _repo;
        private readonly ILogger<RoomService> _logger;

        public RoomService(IRepository<int, Room> repo, ILogger<RoomService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Room> AddRoom(Room room)
        {
            room = await _repo.Add(room);
            return room;
        }

        public async Task<Room> DeleteRoom(int roomId)
        {
            var room = await _repo.Delete(roomId);
            return room;
        }

        public async Task<Room> GetRoom(int roomId)
        {
            var room = await _repo.Get(roomId);
            return room;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            var rooms = await _repo.GetAll();
            return rooms;
        }

        public async Task<RoomDTO> UpdateRoom(RoomDTO roomDTO)
        {
            var room = await _repo.Get(roomDTO.RoomID); // Use RoomID to retrieve the room
            if (room != null)
            {
                room.RoomID = roomDTO.RoomID;
                room.PricePerNight = roomDTO.Price;
                room.IsAvailable = roomDTO.IsAvailable;

                room = await _repo.Update(room);

                return new RoomDTO
                {
                    RoomID = room.RoomID,
                    Type = room.Type,
                    Price = room.PricePerNight,
                    IsAvailable = room.IsAvailable
                };
            }
            throw new RoomNotFoundException();
        }

        public async Task<List<Room>> GetAvailableRoomsByHotel(int hotelId, DateTime checkInDate, DateTime checkOutDate)
        {
            var rooms = await _repo.GetAll();
            return rooms.Where(r => r.HotelID == hotelId && r.Reservations.All(res => checkOutDate <= res.CheckInDate || checkInDate >= res.CheckOutDate)).ToList();
        }

        public async Task<List<Room>> GetRoomsByHotel(int hotelId)
        {
            var rooms = await _repo.GetAll();
            return rooms.Where(r => r.HotelID == hotelId).ToList();
        }

        public async Task<List<Room>> GetRoomsByType(string roomType)
        {
            var rooms = await _repo.GetAll();
            return rooms.Where(r => string.Equals(r.Type.ToString(), roomType, StringComparison.OrdinalIgnoreCase)).ToList();
        }

    }
}