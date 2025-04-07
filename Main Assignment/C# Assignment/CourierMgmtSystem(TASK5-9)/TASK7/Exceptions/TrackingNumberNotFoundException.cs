using System;

namespace myexceptions
{
    public class TrackingNumberNotFoundException : Exception
    {
        public TrackingNumberNotFoundException() : base("Tracking number not found.") { }

        public TrackingNumberNotFoundException(string message) : base(message) { }
    }
}