using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem;


public enum RoomType
{
    Single,
    Double,
    Suite
}

public class Room
{
    public int RoomNumber { get; }
    public RoomType RoomType { get; }
    public decimal NightlyRate { get; private set; }
    public bool IsUnderMaintenance { get; private set; }

    public Room(int roomNumber, RoomType roomType, decimal nightlyRate)
    {
        if (nightlyRate <= 0)
            throw new ArgumentException(
                "Nightly rate must be greater than zero.");

        RoomNumber = roomNumber;
        RoomType = roomType;
        NightlyRate = nightlyRate;
        IsUnderMaintenance = false;
    }

    public void ChangeNightlyRate(decimal newRate)
    {
        if (newRate <= 0)
            throw new ArgumentException(
                "Nightly rate must be greater than zero.");

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
