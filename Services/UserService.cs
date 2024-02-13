using CozyHaven.Exceptions;
using CozyHaven.Mappers;
using CozyHaven.Models.DTOs;
using System.Security.Cryptography;
using System.Text;
using CozyHaven.Interfaces;
using CozyHaven.Models;

namespace CozyHaven.Services
{
    public class UserService : IUserAdminService, IUserCustomerService, IUserHotelOwnerService
    {
        private readonly IRepository<int, User> _repo;
        private readonly ILogger<UserService> _logger;

        public UserService(IRepository<int, User> repo, ILogger<UserService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<User> AddUser(User user)
        {
            user = await _repo.Add(user);
            return user;
        }

        public async Task<User> DeleteUser(int userId)
        {
            var user = await _repo.Delete(userId);
            return user;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _repo.Get(userId);
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _repo.GetAll();
            return users;
        }

        public async Task<UserDTO> UpdateUser(UserDTO userDTO)
        {
            var user = await _repo.Get(userDTO.Id);
            if (user != null)
            {
                // Update user properties with data from userDTO
                user.Username = userDTO.Name;

                // Convert password from string to byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(userDTO.Password);
                user.Password = passwordBytes;

                // Update the user entity in the repository
                user = await _repo.Update(user);

                // Return the updated user DTO
                return new UserDTO
                {
                    Id = user.UserID,
                    Name = user.Username,
                    Password = userDTO.Password // Return the password as a string in the DTO
                };
            }
            else
            {
                throw new ApplicationUserNotFoundException();
            }
        }
        //public async Task<List<User>> GetUsersByHotel(int hotelId)
        //{
        //    var users = await _repo.GetAll();
        //    return users.Where(u => u.HotelOwner != null && u.HotelOwner.Hotels.Any(h => h.HotelID == hotelId)).ToList();
        //}
    }
}
