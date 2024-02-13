using CozyHaven.Exceptions;
using CozyHaven.Interfaces;
using CozyHaven.Mappers;
using CozyHaven.Models.DTOs;
using CozyHaven.Models;
using System.Security.Cryptography;
using System.Text;

namespace CozyHaven.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, Admin> _adminRepository;
        private readonly IRepository<int, HotelOwner> _hotelOwnerRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UserService> _logger;

        public AuthService(IRepository<string, User> userRepository,
                            ITokenService tokenService,
                            ILogger<UserService> logger, IRepository<int,Admin> adminRepository, IRepository<int,HotelOwner> hotelOwnerRepository)
        {
            _adminRepository = adminRepository;
            _hotelOwnerRepository = hotelOwnerRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;

        }

        public async Task<LoginUserDTO> Login(LoginUserDTO user)
        {
            var myUser = await _userRepository.Get(user.Username);
            if (myUser == null)
            {
                throw new InvalidUserException();
            }
            var userPassword = GetPasswordEncrypted(user.Password, myUser.Key);
            var checkPasswordMatch = ComparePasswords(myUser.Password, userPassword);
            if (checkPasswordMatch)
            {
                user.Password = "";
                user.Role = myUser.UserType.ToString();
                user.Token = await _tokenService.GenerateToken(user);
                return user;
            }
            throw new InvalidUserException();
        }

        private bool ComparePasswords(byte[] password, byte[] userPassword)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] != userPassword[i])
                    return false;
            }
            return true;
        }

        private byte[] GetPasswordEncrypted(string password, byte[] key)
        {
            HMACSHA512 hmac = new HMACSHA512(key);
            var userpassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return userpassword;
        }

        public async Task<LoginUserDTO> Register(RegisterUserDTO user)
        {
            User myuser = new RegisterToUser(user).GetUser();
            myuser = await _userRepository.Add(myuser);

            //Checking UserRole
            if(myuser.UserType==UserType.Admin)
            {
                Admin admin = new RegisterToAdmin(myuser).GetAdmin();
                await _adminRepository.Add(admin);
            }
            if (myuser.UserType==UserType.HotelOwner)
            {
                HotelOwner owner = new RegisterToHotelOwner(myuser).GetHotelOwner();
                await _hotelOwnerRepository.Add(owner);
            }
            LoginUserDTO result = new LoginUserDTO
            {
                Username = myuser.Username,
                Role = myuser.UserType.ToString(),
            };
            return result;

        }
    }
}
