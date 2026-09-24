namespace Part2_HotelReservationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HotelReservationSystem hotel = new HotelReservationSystem();

            SeedRooms(hotel);

            bool running = true;
            while (running)
            {
                ShowMenu();
                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": AddGuest(hotel); break;
                        case "2": MakeReservation(hotel); break;
                        case "3": ViewGuestReservations(hotel); break;
                        case "4": ConfirmReservation(hotel); break;
                        case "5": CheckIn(hotel); break;
                        case "6": CheckOut(hotel); break;
                        case "7": CancelReservation(hotel); break;
                        case "8": ChangeRoomRate(hotel); break;
                        case "9": StartRoomMaintenance(hotel); break;
                        case "10": EndRoomMaintenance(hotel); break;
                        case "11": ShowRooms(hotel); break;
                        case "0": running = false; break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static void SeedRooms(HotelReservationSystem hotel)
        {
            hotel.AddRoom(new Room(101, RoomType.Single, 100m));
            hotel.AddRoom(new Room(102, RoomType.Single, 100m));
            hotel.AddRoom(new Room(201, RoomType.Double, 150m));
            hotel.AddRoom(new Room(202, RoomType.Double, 150m));
            hotel.AddRoom(new Room(301, RoomType.Suite, 250m));
        }

        private static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("---------- HOTEL RESERVATION SYSTEM ----------");
            Console.WriteLine();
            Console.WriteLine("1) Add Guest");
            Console.WriteLine("2) Make Reservation");
            Console.WriteLine("3) View Guest Reservations");
            Console.WriteLine("4) Confirm Reservation");
            Console.WriteLine("5) Check In");
            Console.WriteLine("6) Check Out");
            Console.WriteLine("7) Cancel Reservation");
            Console.WriteLine("8) Change Room Rate");
            Console.WriteLine("9) Start Room Maintenance");
            Console.WriteLine("10) End Room Maintenance");
            Console.WriteLine("11) Show Rooms");
            Console.WriteLine("0) Exit");
            Console.Write("Choose an option: ");
        }

        private static void AddGuest(HotelReservationSystem hotel)
        {
            int id = ReadInt("Guest ID: ");
            Console.Write("Full name: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Phone number: ");
            string phone = Console.ReadLine() ?? string.Empty;

            hotel.AddGuest(new Guest(id, name, phone));
            Console.WriteLine("Guest added successfully.");
        }

        private static void MakeReservation(HotelReservationSystem hotel)
        {
            int reservationId = ReadInt("Reservation ID: ");
            Guest guest = FindGuestOrThrow(hotel, ReadInt("Guest ID: "));
            Room room = FindRoomOrThrow(hotel, ReadInt("Room number: "));
            DateTime checkIn = ReadDate("Check-in date (yyyy-MM-dd): ");
            DateTime checkOut = ReadDate("Check-out date (yyyy-MM-dd): ");

            hotel.MakeReservation(reservationId, guest, room, checkIn, checkOut);
            Console.WriteLine("Reservation created successfully (Pending).");
        }

        private static void ViewGuestReservations(HotelReservationSystem hotel)
        {
            Guest guest = FindGuestOrThrow(hotel, ReadInt("Guest ID: "));

            if (guest.Reservations.Count == 0)
            {
                Console.WriteLine("This guest has no reservations.");
                return;
            }

            foreach (Reservation r in guest.Reservations)
            {
                Console.WriteLine(
                    $"ID: {r.ReservationId} | Room: {r.Room.RoomNumber} ({r.Room.RoomType})" +
                    $" | {r.CheckInDate:yyyy-MM-dd} -> {r.CheckOutDate:yyyy-MM-dd}" +
                    $" | Status: {r.Status} | Total: {r.CalculateTotalCost():C}");
            }
        }

        private static void ConfirmReservation(HotelReservationSystem hotel)
        {
            FindReservationOrThrow(hotel, ReadInt("Reservation ID: ")).Confirm();
            Console.WriteLine("Reservation confirmed.");
        }

        private static void CheckIn(HotelReservationSystem hotel)
        {
            FindReservationOrThrow(hotel, ReadInt("Reservation ID: ")).CheckIn();
            Console.WriteLine("Guest checked in.");
        }

        private static void CheckOut(HotelReservationSystem hotel)
        {
            FindReservationOrThrow(hotel, ReadInt("Reservation ID: ")).CheckOut();
            Console.WriteLine("Guest checked out.");
        }

        private static void CancelReservation(HotelReservationSystem hotel)
        {
            FindReservationOrThrow(hotel, ReadInt("Reservation ID: ")).Cancel();
            Console.WriteLine("Reservation cancelled.");
        }

        private static void ChangeRoomRate(HotelReservationSystem hotel)
        {
            Room room = FindRoomOrThrow(hotel, ReadInt("Room number: "));
            decimal rate = ReadDecimal("New nightly rate: ");

            room.ChangeNightlyRate(rate);
            Console.WriteLine($"Room {room.RoomNumber} rate updated to {rate:C}.");
        }

        private static void StartRoomMaintenance(HotelReservationSystem hotel)
        {
            Room room = FindRoomOrThrow(hotel, ReadInt("Room number: "));
            room.StartMaintenance();
            Console.WriteLine($"Room {room.RoomNumber} is now under maintenance.");
        }

        private static void EndRoomMaintenance(HotelReservationSystem hotel)
        {
            Room room = FindRoomOrThrow(hotel, ReadInt("Room number: "));
            room.EndMaintenance();
            Console.WriteLine($"Room {room.RoomNumber} maintenance ended.");
        }

        private static void ShowRooms(HotelReservationSystem hotel)
        {
            foreach (Room room in hotel.Rooms)
            {
                string status;
                if (room.IsUnderMaintenance)
                    status = "Under Maintenance";
                else if (hotel.IsRoomBooked(room))
                    status = "Booked";
                else
                    status = "Available";

                Console.WriteLine(
                    $"Room {room.RoomNumber} | {room.RoomType}" +
                    $" | {room.NightlyRate:C}/night | {status}");
            }
        }

        private static Guest FindGuestOrThrow(HotelReservationSystem hotel, int guestId)
        {
            return hotel.FindGuest(guestId)
                ?? throw new InvalidOperationException($"Guest ID {guestId} not found.");
        }

        private static Room FindRoomOrThrow(HotelReservationSystem hotel, int roomNumber)
        {
            return hotel.FindRoom(roomNumber)
                ?? throw new InvalidOperationException($"Room {roomNumber} not found.");
        }

        private static Reservation FindReservationOrThrow(
            HotelReservationSystem hotel, int reservationId)
        {
            foreach (Guest guest in hotel.Guests)
            {
                foreach (Reservation reservation in guest.Reservations)
                {
                    if (reservation.ReservationId == reservationId)
                        return reservation;
                }
            }

            throw new InvalidOperationException(
                $"Reservation ID {reservationId} not found.");
        }

        private static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;

                Console.WriteLine("Please enter a valid whole number.");
            }
        }

        private static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal value))
                    return value;

                Console.WriteLine("Please enter a valid amount.");
            }
        }

        private static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime value))
                    return value;

                Console.WriteLine("Please enter a valid date (e.g. 2025-06-15).");
            }
        }
    }
}
