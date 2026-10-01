namespace Part2_HotelReservationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var system = new HotelReservationSystem();

            #region All Guest Scenarios

            // Scenario 1: Create a valid Guest.
            // Verifies that a Guest can be created with valid data.

            var guest1 = new Guest(
                1,
                "Ahmed Ali",
                "01012345678");

            Console.WriteLine("Scenario 1: Valid Guest created successfully.");

            // Scenario 2: Guest identity fields are readable.
            // Verifies that GuestId, FullName, and PhoneNumber
            // can be accessed from outside the Guest class.

            Console.WriteLine($"Guest ID: {guest1.GuestId}");
            Console.WriteLine($"Guest Name: {guest1.FullName}");
            Console.WriteLine($"Guest Phone: {guest1.PhoneNumber}");


            // Scenario 3: Register a valid Guest in the system.
            // Verifies that a Guest can be added to the system.

            system.AddGuest(guest1);

            Console.WriteLine("Scenario 3: Guest added to the system.");


            // Scenario 4: Reject a Guest with an empty FullName.
            // Verifies that the Guest constructor prevents
            // creating a Guest without a name.

            try
            {
                var invalidGuest = new Guest(
                    2,
                    "",
                    "01011111111");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 4: Expected error - {ex.Message}");
            }


            // Scenario 5: Reject a Guest with a null FullName.
            // Verifies that null is not accepted as a Guest name.

            try
            {
                var invalidGuest = new Guest(
                    3,
                    null!,
                    "01011111111");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 5: Expected error - {ex.Message}");
            }



            // Scenario 6: Reject a Guest with an empty PhoneNumber.
            // Verifies that the Guest constructor prevents
            // creating a Guest without a phone number.

            try
            {
                var invalidGuest = new Guest(
                    4,
                    "Mohamed Hassan",
                    "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 6: Expected error - {ex.Message}");
            }


            // Scenario 7: Reject a Guest with a null PhoneNumber.
            // Verifies that null is not accepted as a phone number.

            try
            {
                var invalidGuest = new Guest(
                    5,
                    "Mohamed Hassan",
                    null!);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 7: Expected error - {ex.Message}");
            }


            // Scenario 8: Reject a duplicate GuestId.
            // Verifies that two Guests cannot be registered
            // with the same GuestId in the same system.

            try
            {
                var duplicateGuest = new Guest(
                    1,
                    "Omar Hassan",
                    "01022222222");

                system.AddGuest(duplicateGuest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 8: Expected error - {ex.Message}");
            }


            // Scenario 9: Register another valid Guest.
            // Verifies that the system can contain multiple Guests
            // as long as their GuestIds are unique.

            var guest2 = new Guest(
                2,
                "Omar Hassan",
                "01022222222");

            system.AddGuest(guest2);

            Console.WriteLine(
                "Scenario 9: Second Guest added successfully.");


            // Scenario 10: A Guest can have multiple Reservations.
            // Verifies that one Guest can make many reservations
            // over time.


            var room101 = new Room(
                101,
                RoomType.Single,
                500);

            var room102 = new Room(
                102,
                RoomType.Double,
                700);

            var reservation1 = system.CreateReservation(
                1001,
                guest1,
                room101,
                new DateTime(2026, 10, 1),
                new DateTime(2026, 10, 3));

            var reservation2 = system.CreateReservation(
                1002,
                guest1,
                room102,
                new DateTime(2026, 10, 5),
                new DateTime(2026, 10, 8));

            Console.WriteLine(
                "Scenario 10: Guest successfully created multiple reservations.");


            // Scenario 11: Guest can view the full reservation history.
            // Verifies that all reservations belonging to the Guest
            // are available through the Reservations property.

            Console.WriteLine(
                $"Scenario 11: {guest1.FullName}'s reservation history:");

            foreach (var reservation in guest1.Reservations)
            {
                Console.WriteLine(
                    $"Reservation ID: {reservation.ReservationId}");
            }


            // Scenario 12: Cancelled reservations remain in history.
            // Verifies that cancelling a reservation does not remove
            // it from the Guest's reservation history.

            reservation1.Cancel();

            Console.WriteLine(
                $"Scenario 12: Reservation {reservation1.ReservationId} " +
                $"is now {reservation1.Status}.");

            Console.WriteLine(
                $"Reservation history count: {guest1.Reservations.Count}");


            // Scenario 13: Checked-out reservations remain in history.
            // Verifies that checking out a reservation does not remove
            // it from the Guest's reservation history.

            reservation2.Confirm();
            reservation2.CheckIn();
            reservation2.CheckOut();

            Console.WriteLine(
                $"Scenario 13: Reservation {reservation2.ReservationId} " +
                $"is now {reservation2.Status}.");

            Console.WriteLine(
                $"Reservation history count: {guest1.Reservations.Count}");

            #endregion

            #region All Room Scenarios

            // Scenario 1: Create a valid Room.
            // Verifies that a Room can be created with valid data.

            var room1001 = new Room(
                1001,
                RoomType.Single,
                500);

            Console.WriteLine(
                "Scenario 1: Valid Room created successfully.");


            // Scenario 2: Read Room information.
            // Verifies that RoomNumber, RoomType, and NightlyRate
            // are accessible from outside the Room class.

            Console.WriteLine($"Room Number: {room1001.RoomNumber}");
            Console.WriteLine($"Room Type: {room1001.RoomType}");
            Console.WriteLine($"Nightly Rate: {room1001.NightlyRate}");


            // Scenario 3: Reject a Room with a zero nightly rate.
            // Verifies the positive pricing rule during construction.

            try
            {
                var invalidRoom = new Room(
                    102,
                    RoomType.Double,
                    0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 3: Expected error - {ex.Message}");
            }


            // Scenario 4: Reject a Room with a negative nightly rate.
            // Verifies that negative prices cannot create an invalid Room.

            try
            {
                var invalidRoom = new Room(
                    103,
                    RoomType.Suite,
                    -100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 4: Expected error - {ex.Message}");
            }


            // Scenario 5: Change the Room nightly rate.
            // Verifies that the dedicated pricing method can update
            // the Room's nightly rate.

            room1001.ChangeNightlyRate(600);

            Console.WriteLine(
                $"Scenario 5: New nightly rate = {room1001.NightlyRate}");


            // Scenario 6: Reject changing the nightly rate to zero.
            // Verifies that invalid pricing cannot be introduced later.


            try
            {
                room1001.ChangeNightlyRate(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 6: Expected error - {ex.Message}");
            }


            // Scenario 7: Reject changing the nightly rate to a negative value.
            // Verifies that the Room always keeps a positive nightly rate.

            try
            {
                room1001.ChangeNightlyRate(-50);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 7: Expected error - {ex.Message}");
            }


            // Scenario 8: Verify the initial maintenance state.
            // Verifies that a newly created Room is not under maintenance.

            Console.WriteLine(
                $"Scenario 8: Room under maintenance = " +
                $"{room1001.IsUnderMaintenance}");


            // Scenario 9: Start Room maintenance.
            // Verifies that the dedicated maintenance action
            // changes the Room maintenance state.

            room1001.StartMaintenance();

            Console.WriteLine(
                $"Scenario 9: Room under maintenance = " +
                $"{room1001.IsUnderMaintenance}");


            // Scenario 10: End Room maintenance.
            // Verifies that the dedicated maintenance action
            // makes the Room available again.

            room1001.EndMaintenance();

            Console.WriteLine(
                $"Scenario 10: Room under maintenance = " +
                $"{room101.IsUnderMaintenance}");


            // Scenario 11: Room state cannot be modified directly.
            // Verifies that NightlyRate and IsUnderMaintenance
            // are protected by private setters.


            // The following operations are intentionally impossible:
            //
            // room101.NightlyRate = 1000;
            // room101.IsUnderMaintenance = true;
            //
            // Changes must happen through dedicated methods:
            //
            // room101.ChangeNightlyRate(1000);
            // room101.StartMaintenance();
            // room101.EndMaintenance();
            #endregion

            #region All Reservation Scenarios
            // Scenario 1: Create a valid Reservation.
            // Verifies that a Reservation can be created with valid

            var reservation1001 = system.CreateReservation(
                        101,
                        guest1,
                        room101,
                        new DateTime(2026, 10, 1),
                        new DateTime(2026, 10, 4));

            Console.WriteLine(
                "Scenario 1: Valid Reservation created successfully.");

            
            // Scenario 2: Read Reservation information.
            // Verifies that the Reservation's identity and dates
            // can be accessed from outside the class.
            Console.WriteLine(
                $"Reservation ID: {reservation1001.ReservationId}");

            Console.WriteLine(
                $"Check-in: {reservation1001.CheckInDate}");

            Console.WriteLine(
                $"Check-out: {reservation1001.CheckOutDate}");

            // Scenario 3: Verify the Reservation's Guest reference.
            Console.WriteLine(
                $"Reservation Guest: {reservation1001.Guest.FullName}");


            // Scenario 4: Verify the Reservation's Room reference.
            Console.WriteLine(
                $"Reservation Room: {reservation1001.Room.RoomNumber}");

            Console.WriteLine(
                $"Same Room object: {ReferenceEquals(
                    reservation1001.Room,
                    room101)}");

            // Scenario 5: Verify the initial Reservation status.
            Console.WriteLine(
                $"Scenario 5: Initial status = {reservation1001.Status}"); //Pending 

            
            // Scenario 6: Calculate the Reservation total cost.
            // Verifies that TotalCost is calculated automatically
            // from the number of nights and the Room nightly rate.
            Console.WriteLine(
                $"Scenario 6: Total cost = {reservation1001.TotalCost}");


            // Scenario 7: Verify that TotalCost uses the Room's
            // current nightly rate.
            room101.ChangeNightlyRate(700);
            Console.WriteLine(
                $"Scenario 7: Total cost after rate change = " +
                $"{reservation1001.TotalCost}");


            // Scenario 8: Reject a Reservation with the same
            // Check-in and Check-out date.
            try
            {
                system.CreateReservation(
                    1003,
                    guest1,
                    room102,
                    new DateTime(2026, 11, 10),
                    new DateTime(2026, 11, 10));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 8: Expected error - {ex.Message}");
            }

            
            // Scenario 9: Reject a Reservation where Check-out
            // is before Check-in 
            try
            {
                system.CreateReservation(
                    1004,
                    guest1,
                    room102,
                    new DateTime(2026, 11, 15),
                    new DateTime(2026, 11, 10));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 9: Expected error - {ex.Message}");
            }


            // Scenario 10: Reject a Reservation with a null Guest.
            // Verifies that a Reservation cannot exist without a Guest.
            try
            {
                system.CreateReservation(
                    1005,
                    null!,
                    room102,
                    new DateTime(2026, 11, 20),
                    new DateTime(2026, 11, 22));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 10: Expected error - {ex.Message}");
            }

            // Scenario 11: Reject a Reservation with a null Room.
            // Verifies that a Reservation cannot exist without a Room.

            try
            {
                system.CreateReservation(
                    1006,
                    guest1,
                    null!,
                    new DateTime(2026, 11, 20),
                    new DateTime(2026, 11, 22));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 11: Expected error - {ex.Message}");
            }

            // Scenario 12: Reject a Reservation for a Room under maintenance.
            // Verifies that a Room cannot be booked while it is out of service.
            room102.StartMaintenance();

            try
            {
                system.CreateReservation(
                    1006,
                    guest1,
                    room102,
                    new DateTime(2026, 12, 1),
                    new DateTime(2026, 12, 3));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 12: Expected error - {ex.Message}");
            }

            room102.EndMaintenance();

            // Scenario 13: Pending -> Confirmed.
            // Verifies the first valid status transition.
            reservation1001.Confirm();

            Console.WriteLine(
                $"Scenario 13: Status = {reservation1001.Status}");



            // Scenario 14: Reject Check-In while Reservation is Pending.
            // Verifies that confirmation is required before check-in.
            var reservation02 = system.CreateReservation(
                1007,
                guest1,
                room102,
                new DateTime(2026, 12, 10),
                new DateTime(2026, 12, 12));

            try
            {
                reservation02.CheckIn();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 14: Expected error - {ex.Message}");
            }


            // Scenario 15: Confirmed -> CheckedIn.
            // Verifies that a confirmed Reservation can be checked in.
            reservation1001.CheckIn();

            Console.WriteLine(
                $"Scenario 15: Status = {reservation1001.Status}");

            
            // Scenario 16: Reject a second Check-In.
            // Verifies that CheckedIn cannot transition to CheckedIn again.
            try
            {
                reservation1001.CheckIn();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 16: Expected error - {ex.Message}");
            }

            
            // Scenario 17: CheckedIn -> CheckedOut.
            // Verifies the valid checkout transition.
            reservation1001.CheckOut();

            Console.WriteLine(
                $"Scenario 17: Status = {reservation1001.Status}");



            // Scenario 18: Reject Check-Out while Reservation is Pending.
            // Verifies that a Reservation must be CheckedIn before checkout.
            try
            {
                reservation2.CheckOut();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 18: Expected error - {ex.Message}");
            }

            // Scenario 19: Reject a second Check-Out.
            // Verifies that CheckedOut cannot transition to CheckedOut again.
            try
            {
                reservation1001.CheckOut();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 19: Expected error - {ex.Message}");
            }


            // Scenario 20: Pending -> Cancelled.
            // Verifies that a pending Reservation can be cancelled.
            reservation02.Cancel();

            Console.WriteLine(
                $"Scenario 20: Status = {reservation02.Status}");

            // Scenario 21: Confirmed -> Cancelled.
            // Verifies that a confirmed Reservation can be cancelled.
            var reservation3 = system.CreateReservation(
                1008,
                guest1,
                room102,
                new DateTime(2027, 1, 10),
                new DateTime(2027, 1, 12));
            reservation3.Confirm();
            reservation3.Cancel();

            Console.WriteLine(
                $"Scenario 21: Status = {reservation3.Status}");


            // Scenario 22: Reject CheckedIn -> Cancelled.
            // Verifies that an already checked-in Reservation
            // cannot be cancelled.

            var reservation4 = system.CreateReservation(
                1009,
                guest1,
                room102,
                new DateTime(2027, 2, 10),
                new DateTime(2027, 2, 12));

            reservation4.Confirm();
            reservation4.CheckIn();

            try
            {
                reservation4.Cancel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 22: Expected error - {ex.Message}");
            }


            // Scenario 23: Reject CheckedOut -> Cancelled.
            // Verifies that a completed Reservation cannot be cancelled
            reservation4.CheckOut();
            try
            {
                reservation4.Cancel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 23: Expected error - {ex.Message}");
            }

            // Scenario 24: Reject Cancelled -> Confirmed.
            // Verifies that a cancelled Reservation cannot be confirmed again.
            var reservation5 = system.CreateReservation(
                1010,
                guest1,
                room102,
                new DateTime(2027, 3, 10),
                new DateTime(2027, 3, 12));

            reservation5.Cancel();

            try
            {
                reservation5.Confirm();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 24: Expected error - {ex.Message}");
            }

            // Scenario 25: Reject Cancelled -> CheckedIn.
            // Verifies that a cancelled Reservation cannot be checked in.

            try
            {
                reservation5.CheckIn();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 25: Expected error - {ex.Message}");
            }


            // Scenario 26: Reject Cancelled -> CheckedOut.
            // Verifies that a cancelled Reservation cannot be checked out.

            try
            {
                reservation5.CheckOut();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 26: Expected error - {ex.Message}");
            }

            // ---------------------------------------------------------
            // Scenario 27: Reservation identity and dates cannot be
            // modified directly.
            // ---------------------------------------------------------

            // The following operations are intentionally impossible:
            //
            // reservation1.ReservationId = 2000;
            // reservation1.CheckInDate = new DateTime(2027, 5, 1);
            // reservation1.CheckOutDate = new DateTime(2027, 5, 5);
            //
            // These properties are get-only and can only be assigned
            // during Reservation construction.
            #endregion

            #region All HotelReservationSystem Scenarios
            // Scenario 1: Add a valid Guest to the system.
            // Verifies that the system can register a valid Guest.
            var guest10 = new Guest(
                10,
                "Ahmed Ali",
                "01012345678");

            system.AddGuest(guest10);

            Console.WriteLine(
                "Scenario 1: Guest added successfully.");


            // Scenario 2: Add multiple Guests.
            // Verifies that different Guests can be registered
            var guest11 = new Guest(
                11,
                "Omar Hassan",
                "01022222222");

            system.AddGuest(guest11);

            Console.WriteLine(
                "Scenario 2: Multiple Guests registered successfully.");

            // Scenario 3: Reject a duplicate GuestId.
            // Verifies that the system prevents two Guests from
            // having the same GuestId.
            try
            {
                var duplicateGuest = new Guest(
                    10,
                    "Mohamed Hassan",
                    "01033333333");

                system.AddGuest(duplicateGuest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 3: Expected error - {ex.Message}");
            }

            
            // Scenario 4: Create a valid Reservation through the system.
            // Verifies that HotelReservationSystem creates and registers
            // a Reservation correctly.
            var room1005 = new Room(
                1055,
                RoomType.Single,
                500);

            var reservation1005 = system.CreateReservation(
                1005,
                guest10,
                room1005,
                new DateTime(2026, 10, 1),
                new DateTime(2026, 10, 4));

            Console.WriteLine(
                "Scenario 4: Reservation created successfully.");


            // Scenario 5: Verify that the Reservation is stored
            // in the system's reservation collection.
            Console.WriteLine(
                $"Scenario 5: Total system reservations = " +
                $"{system.Reservations.Count}");

            // Scenario 6: Reject a duplicate ReservationId.
            // Verifies that the system prevents two Reservations
            // from having the same ReservationId.
            try
            {
                system.CreateReservation(
                    1001,
                    guest1,
                    room101,
                    new DateTime(2026, 10, 5),
                    new DateTime(2026, 10, 7));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 6: Expected error - {ex.Message}");
            }


            // Scenario 7: Reject a Reservation for an unregistered Guest.
            // Verifies that the system only creates Reservations for
            // Guests registered in the system.
            var unregisteredGuest = new Guest(
                50,
                "Unregistered Guest",
                "01099999999");

            try
            {
                system.CreateReservation(
                    1002,
                    unregisteredGuest,
                    room101,
                    new DateTime(2026, 10, 5),
                    new DateTime(2026, 10, 7));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 7: Expected error - {ex.Message}");
            }


            // Scenario 8: Reject an overlapping active Reservation.
            // Verifies the double-booking rule.

            var reservation10004 = system.CreateReservation(
                10004,
                guest1,
                room101,
                new DateTime(2026, 10, 1),
                new DateTime(2026, 10, 5));
            try
            {
                system.CreateReservation(
                    10005,
                    guest1,
                    room101,
                    new DateTime(2026, 10, 3),
                    new DateTime(2026, 10, 6));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 8: Expected error - {ex.Message}");
            }

            // Scenario 9: Reject a Reservation completely inside
            // an existing active Reservation.
            // Verifies another overlapping date-range case.

            try
            {
                system.CreateReservation(
                    1004,
                    guest2,
                    room101,
                    new DateTime(2026, 10, 2),
                    new DateTime(2026, 10, 3));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 9: Expected error - {ex.Message}");
            }

            // Scenario 10: Reject a Reservation that completely contains
            // an existing active Reservation.
            // Verifies the reverse overlapping date-range case.

            try
            {
                system.CreateReservation(
                    1005,
                    guest2,
                    room101,
                    new DateTime(2026, 9, 30),
                    new DateTime(2026, 10, 5));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 10: Expected error - {ex.Message}");
            }


            // Scenario 11: Allow an adjacent Reservation.
            // Verifies that a new Reservation can start exactly when
            // the previous Reservation ends because the date ranges
            // do not overlap.
            var reservation20 = system.CreateReservation(
                1015,
                guest2,
                room1001,
                new DateTime(2026, 10, 4),
                new DateTime(2026, 10, 7));
            Console.WriteLine(
                "Scenario 11: Adjacent Reservation created successfully.");

            // Scenario 12: A cancelled Reservation does not block
            // a Room from being booked again.
            var reservation30 = system.CreateReservation(
                1030,
                guest1,
                room101,
                new DateTime(2026, 10, 10),
                new DateTime(2026, 10, 13));

            reservation30.Cancel();

            Console.WriteLine(
                $"Reservation {reservation30.ReservationId} status: " +
                $"{reservation30.Status}");


            // Scenario 13: A checked-out Reservation does not block
            // a Room from being booked again.
            var reservation50 = system.CreateReservation(
                1090,
                guest1,
                room101,
                new DateTime(2026, 10, 15),
                new DateTime(2026, 10, 18));

            reservation50.Confirm();
            reservation50.CheckIn();
            reservation50.CheckOut();

            Console.WriteLine(
                $"Reservation {reservation50.ReservationId} status: " +
                $"{reservation5.Status}");


            // Scenario 13 continued: Create another Reservation
            // with overlapping dates after the previous Reservation
            // has been checked out.
            var reservation6 = system.CreateReservation(
                1060,
                guest2,
                room101,
                new DateTime(2026, 10, 16),
                new DateTime(2026, 10, 19));

            Console.WriteLine(
                "Scenario 13: Reservation created after checkout.");

            // ---------------------------------------------------------
            // Scenario 14: Reject a Reservation for a Room under maintenance.
            // Verifies that the system cannot book a Room while it is
            // out of service.
            // ---------------------------------------------------------

            var room1002 = new Room(
                102,
                RoomType.Double,
                700);

            room1002.StartMaintenance();

            try
            {
                system.CreateReservation(
                    1011,
                    guest1,
                    room1002,
                    new DateTime(2026, 11, 1),
                    new DateTime(2026, 11, 3));
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Scenario 14: Expected error - {ex.Message}");
            }

            room102.EndMaintenance();

            // Scenario 15: Verify that created Reservations are also
            // added to the Guest's reservation history.

            Console.WriteLine(
                $"Scenario 15: {guest1.FullName} has " +
                $"{guest1.Reservations.Count} reservations in history.");


            // Scenario 16: Verify that the system keeps the complete
            // Reservation collection.
            Console.WriteLine(
                $"Scenario 16: System has " +
                $"{system.Reservations.Count} reservations.");

            #endregion



        }
    }
}
