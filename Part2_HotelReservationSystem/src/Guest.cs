using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem
{
   public  class Guest
    {
        private readonly List<Reservation> _reservations = new();

        public int GuestId { get; }

        public string FullName { get; }

        public string PhoneNumber { get; }

        public IReadOnlyList<Reservation> Reservations => _reservations;

        public Guest(int guestId, string fullName, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException(
                    "Guest full name cannot be empty.",
                    nameof(fullName));
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException(
                    "Guest phone number cannot be empty.",
                    nameof(phoneNumber));
            }

            GuestId = guestId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }

        public void AddReservation(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException(nameof(reservation));
            }

            _reservations.Add(reservation);
        }
    }
}
