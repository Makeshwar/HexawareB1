using System;
namespace CourierManagementSystem.Entities
{
	public class Location
	{
        private int locationID;
        private string locationName;
        private string address;

        public Location() { }

        public Location(int id, string name, string address)
        {
            this.locationID = id;
            this.locationName = name;
            this.address = address;
        }

        public int LocationID { get => locationID; set => locationID = value; }
        public string LocationName { get => locationName; set => locationName = value; }
        public string Address { get => address; set => address = value; }

        public override string ToString()
        {
            return $"Location [ID={locationID}, Name={locationName}, Address={address}]";
        }
    }
}

