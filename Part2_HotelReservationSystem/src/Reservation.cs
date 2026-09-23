using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem;

public enum ReservationStatus
{
    Pending,
    Confirmed,
    CheckedIn,
    CheckedOut,
    Cancelled
}

public class Reservation
{
    public int ReservationId { get; }
    public Guest Guest { get; }
    public Room Room { get; }
    public DateTime CheckInDate { get; }
    public DateTime CheckOutDate { get; }
    public ReservationStatus Status { get; private set; }

    public Reservation(
        int reservationId,
        Guest guest,
        Room room,
        DateTime checkInDate,
        DateTime checkOutDate)
    {
        if (checkOutDate <= checkInDate)
            throw new ArgumentException(
                "Check-out date must be after check-in date.");

        if (room.IsUnderMaintenance)
            throw new InvalidOperationException(
                "Cannot create a reservation for a room under maintenance.");

        ReservationId = reservationId;
        Guest = guest;
        Room = room;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Status = ReservationStatus.Pending;
    }

    public void Confirm()
    {
        if (Status != ReservationStatus.Pending)
            throw new InvalidOperationException(
                "Only pending reservations can be confirmed.");

        Status = ReservationStatus.Confirmed;
    }

    public void CheckIn()
    {
        if (Status != ReservationStatus.Confirmed)
            throw new InvalidOperationException(
                "Only confirmed reservations can be checked in.");

        Status = ReservationStatus.CheckedIn;
    }

    public void CheckOut()
    {
        if (Status != ReservationStatus.CheckedIn)
            throw new InvalidOperationException(
                "Only checked-in reservations can be checked out.");

        Status = ReservationStatus.CheckedOut;
    }

    public void Cancel()
    {
        if (Status != ReservationStatus.Pending &&
            Status != ReservationStatus.Confirmed)
        {
            throw new InvalidOperationException(
                "Only pending or confirmed reservations can be cancelled.");
        }

        Status = ReservationStatus.Cancelled;
    }

    public decimal CalculateTotalCost()
    {
        int nights = (CheckOutDate - CheckInDate).Days;

        return nights * Room.NightlyRate;
    }
}
