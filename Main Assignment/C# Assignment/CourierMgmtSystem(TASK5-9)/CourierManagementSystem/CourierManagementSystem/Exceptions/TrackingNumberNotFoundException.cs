using System;

namespace CourierManagementSystem.Exceptions
{
    public class TrackingNumberNotFoundException : Exception
    {
        public TrackingNumberNotFoundException() : base("Tracking number not found.") { }

        public TrackingNumberNotFoundException(string message) : base(message) { }
    }
}