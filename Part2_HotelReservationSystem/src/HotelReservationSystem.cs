using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem;

public class HotelReservationSystem
{
    private readonly List<Guest> _guests = new();
    private readonly List<Room> _rooms = new();

    public IReadOnlyList<Guest> Guests => _guests;
    public IReadOnlyList<Room> Rooms => _rooms;

    public void AddGuest(Guest guest)
    {
        if (FindGuest(guest.GuestId) != null)
            throw new InvalidOperationException(
                $"Guest ID {guest.GuestId} already exists.");

        _guests.Add(guest);
    }

    public void AddRoom(Room room)
    {
        if (FindRoom(room.RoomNumber) != null)
            throw new InvalidOperationException(
                $"Room {room.RoomNumber} already exists.");

        _rooms.Add(room);
    }

    public Guest? FindGuest(int guestId)
    {
        return _guests.FirstOrDefault(
            guest => guest.GuestId == guestId);
    }

    public Room? FindRoom(int roomNumber)
    {
        return _rooms.FirstOrDefault(
            room => room.RoomNumber == roomNumber);
    }

    public bool IsReservationIdAvailable(int reservationId)
    {
        foreach (Guest guest in _guests)
        {
            if (guest.Reservations.Any(
                reservation => reservation.ReservationId == reservationId))
            {
                return false;
            }
        }

        return true;
    }

    public bool IsRoomAvailable(
        Room room,
        DateTime checkInDate,
        DateTime checkOutDate)
    {
        if (room.IsUnderMaintenance)
            return false;

        foreach (Guest guest in _guests)
        {
            foreach (Reservation reservation in guest.Reservations)
            {
                if (reservation.Room != room)
                    continue;

                if (reservation.Status == ReservationStatus.Cancelled ||
                    reservation.Status == ReservationStatus.CheckedOut)
                {
                    continue;
                }

                bool overlaps =
                    checkInDate < reservation.CheckOutDate &&
                    reservation.CheckInDate < checkOutDate;

                if (overlaps)
                    return false;
            }
        }

        return true;
    }
}