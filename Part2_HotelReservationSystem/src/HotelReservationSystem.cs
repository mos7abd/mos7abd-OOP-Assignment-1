using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem
{
    // This class is responsible for managing reservations and preventing double-booking
    // because this rule requires access to multiple reservations across the system.

    // The double-booking logic could also be placed inside the Room class,
    // but keeping it here avoids making Room responsible for managing reservation history
    public class HotelReservationSystem
    {
        private readonly List<Reservation> _reservations = new();
        private readonly List<Guest> _guests = new();
        public IReadOnlyList<Reservation> Reservations => _reservations;
        public IReadOnlyList<Guest> Guests => _guests;

        private bool HasOverlappingReservation( Room room, DateTime checkInDate,DateTime checkOutDate)
                {
            return _reservations.Any(reservation =>
                reservation.Room == room &&
                reservation.Status != ReservationStatus.Cancelled &&
                reservation.Status != ReservationStatus.CheckedOut &&
                checkInDate < reservation.CheckOutDate &&
                checkOutDate > reservation.CheckInDate);
        }

        public Reservation CreateReservation(int reservationId,Guest guest,Room room,
            DateTime checkInDate,DateTime checkOutDate)
        {
            if (guest == null)
            {
                throw new ArgumentNullException(nameof(guest));
            }

            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            if (!_guests.Contains(guest))
            {
                throw new InvalidOperationException(
                    "The guest must be registered in the hotel reservation system.");
            }

            if (_reservations.Any(
            reservation => reservation.ReservationId == reservationId))
            {
                throw new InvalidOperationException(
                    "A reservation with the same Reservation ID already exists.");
            }
            if (HasOverlappingReservation(room, checkInDate, checkOutDate))
            {
                throw new InvalidOperationException(
                    "The room already has an overlapping active reservation.");
            }

            var reservation = new Reservation(
                reservationId,
                guest,
                room,
                checkInDate,
                checkOutDate);

            _reservations.Add(reservation);
            guest.AddReservation(reservation);

            return reservation;
        }

        public void AddGuest(Guest guest)
        {
            if (_guests.Any(existingGuest => existingGuest.GuestId == guest.GuestId))
            {
                throw new InvalidOperationException(
                    "A guest with the same Guest ID already exists.");
            }

            _guests.Add(guest);
        }
    }
}
