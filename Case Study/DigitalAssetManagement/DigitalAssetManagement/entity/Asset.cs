namespace entity
{
    public class Asset
    {
        private int assetId;

        private string name;

        private string type;

        private string serialNumber;

        private DateTime purchaseDate;

        private string location;

        private string status;

        private int? ownerId;

        public Asset() {

        }

        public Asset(int assetId, string name, string type, string serialNumber, DateTime purchaseDate, string location, string status, int? ownerId)

        {
            this.assetId = assetId;
            this.name = name;

            this.type = type;

            this.serialNumber = serialNumber;

            this.purchaseDate = purchaseDate;

            this.location = location;

            this.status = status;

            this.ownerId = ownerId;
        }

        public int AssetId { get => assetId; set => assetId = value; }

        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }

        public string SerialNumber { get => serialNumber; set => serialNumber = value; }

        public DateTime PurchaseDate { get => purchaseDate; set => purchaseDate = value; }

        public string Location { get => location; set => location = value; }

        public string Status { get => status; set => status = value; }

        public int? OwnerId { get => ownerId; set => ownerId = value; }
   }
}