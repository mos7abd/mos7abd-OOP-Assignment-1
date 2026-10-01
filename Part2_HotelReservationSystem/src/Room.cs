using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem
{
     public class Room
    {
        public int RoomNumber { get; }

        public RoomType RoomType { get; }

        public double NightlyRate { get; private set; }

        public bool IsUnderMaintenance { get; private set; }

        public Room(int roomNumber, RoomType roomType, double nightlyRate)
        {
            if (nightlyRate <= 0)
            {
                throw new ArgumentException(
                    "Nightly rate must be greater than zero.",
                    nameof(nightlyRate));
            }

            RoomNumber = roomNumber;
            RoomType = roomType;
            NightlyRate = nightlyRate;
            IsUnderMaintenance = false;
        }

        public void ChangeNightlyRate(double newRate)
        {
            if (newRate <= 0)
            {
                throw new ArgumentException(
                    "Nightly rate must be greater than zero.",
                    nameof(newRate));
            }

            NightlyRate = newRate;
        }

        public void StartMaintenance()
        {
            IsUnderMaintenance = true;
        }

        public void EndMaintenance()
        {
            IsUnderMaintenance = false;
        }
    }
}
