using CozyHaven.Models.DTOs;
using CozyHaven.Exceptions;
using CozyHaven.Interfaces;
using CozyHaven.Models.DTOs;
using CozyHaven.Models;

namespace CozyHaven.Services
{
    public class ReservationService : IReservationAdminService, IReservationCustomerService, IReservationHotelService
    {
        private readonly IRepository<int, Reservation> _repo;
        private readonly ILogger<ReservationService> _logger;

        public ReservationService(IRepository<int, Reservation> repo, ILogger<ReservationService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Reservation> AddReservation(Reservation reservation)
        {
            reservation = await _repo.Add(reservation);
            return reservation;
        }

        public async Task<Reservation> DeleteReservation(int reservationId)
        {
            var reservation = await _repo.Delete(reservationId);
            return reservation;
        }

        public async Task<Reservation> GetReservation(int reservationId)
        {
            var reservation = await _repo.Get(reservationId);
            return reservation;
        }

        public async Task<List<Reservation>> GetAllReservations()
        {
            var reservations = await _repo.GetAll();
            return reservations;
        }

        public async Task<ReservationDTO> UpdateReservation(ReservationDTO reservationDTO)
        {
            var reservation = await _repo.Get(reservationDTO.ReservationID);
            if (reservation != null)
            {
                reservation.CheckInDate = reservationDTO.CheckInDate;
                reservation.CheckOutDate = reservationDTO.CheckOutDate;
                reservation = await _repo.Update(reservation);
                return new ReservationDTO
                {
                    ReservationID = reservation.ReservationID,
                    CheckInDate = reservation.CheckInDate,
                    CheckOutDate = reservation.CheckOutDate,
                };
            }
            throw new ReservationNotFoundException();
        }

        public async Task<List<Reservation>> GetAllReservationsByUser(int userId)
        {
            var reservations = await _repo.GetAll();
            return reservations.Where(r => r.UserID == userId).ToList();
        }

        public async Task<List<Reservation>> GetAllReservationsByHotel(int hotelId)
        {
            var reservations = await _repo.GetAll();
            return reservations.Where(r => r.Room.HotelID == hotelId).ToList();
        }
    }
}