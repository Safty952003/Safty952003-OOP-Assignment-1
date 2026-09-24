using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem;

public class Guest
{
    private readonly List<Reservation> _reservations = new();

    public int GuestId { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }

    public IReadOnlyList<Reservation> Reservations => _reservations;

    public Guest(int guestId, string fullName, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException(
                "Guest full name cannot be empty.");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException(
                "Guest phone number cannot be empty.");

        GuestId = guestId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public Reservation AddReservation(
    int reservationId,
    Room room,
    DateTime checkInDate,
    DateTime checkOutDate)
    {
        Reservation reservation = new Reservation(
            reservationId,
            this,
            room,
            checkInDate,
            checkOutDate);

        _reservations.Add(reservation);

        return reservation;
    }
}
