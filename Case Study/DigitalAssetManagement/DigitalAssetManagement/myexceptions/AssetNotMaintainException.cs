using System;

namespace myexceptions
{
    public class AssetNotMaintainException : Exception
    {
        public AssetNotMaintainException() : base() { }

        public AssetNotMaintainException(string message) : base(message) { }

        public AssetNotMaintainException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}