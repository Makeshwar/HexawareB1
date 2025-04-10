using System;

namespace myexceptions
{
    public class AssetNotFoundException : Exception
    {
        public AssetNotFoundException() : base() { }

        public AssetNotFoundException(string message) : base(message) { }

        public AssetNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}