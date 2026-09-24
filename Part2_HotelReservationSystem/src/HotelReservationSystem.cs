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
        {
            throw new InvalidOperationException(
                $"Guest ID {guest.GuestId} already exists.");
        }

        _guests.Add(guest);
    }

    public void AddRoom(Room room)
    {
        if (FindRoom(room.RoomNumber) != null)
        {
            throw new InvalidOperationException(
                $"Room {room.RoomNumber} already exists.");
        }

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

    public Reservation MakeReservation(
        int reservationId,
        Guest guest,
        Room room,
        DateTime checkInDate,
        DateTime checkOutDate)
    {
        ValidateReservation(
            reservationId,
            room,
            checkInDate,
            checkOutDate);

        return guest.AddReservation(
            reservationId,
            room,
            checkInDate,
            checkOutDate);
    }

    public bool IsRoomBooked(Room room)
    {
        foreach (Guest guest in _guests)
        {
            foreach (Reservation reservation in guest.Reservations)
            {
                if (reservation.Room == room &&
                    reservation.Status != ReservationStatus.Cancelled &&
                    reservation.Status != ReservationStatus.CheckedOut)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void ValidateReservation(
        int reservationId,
        Room room,
        DateTime checkInDate,
        DateTime checkOutDate)
    {
        if (!IsReservationIdAvailable(reservationId))
        {
            throw new InvalidOperationException(
                $"Reservation ID {reservationId} already exists.");
        }

        if (HasOverlappingReservation(
                room,
                checkInDate,
                checkOutDate))
        {
            throw new InvalidOperationException(
                $"Room {room.RoomNumber} is already booked for these dates.");
        }
    }

    private bool IsReservationIdAvailable(int reservationId)
    {
        foreach (Guest guest in _guests)
        {
            foreach (Reservation reservation in guest.Reservations)
            {
                if (reservation.ReservationId == reservationId)
                    return false;
            }
        }

        return true;
    }

    private bool HasOverlappingReservation(
        Room room,
        DateTime checkInDate,
        DateTime checkOutDate)
    {
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
                    return true;
            }
        }

        return false;
    }
}