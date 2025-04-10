namespace entity
{
    public class MaintenanceRecord
    {
        private int maintenanceId;
        private int assetId;
        private DateTime maintenanceDate;
        private string description;
        private double cost;

        public MaintenanceRecord() { }

        public MaintenanceRecord(int maintenanceId, int assetId, DateTime maintenanceDate, string description, double cost)
        {
            this.maintenanceId = maintenanceId;
            this.assetId = assetId;
            this.maintenanceDate = maintenanceDate;
            this.description = description;
            this.cost = cost;
        }

        public int MaintenanceId { get => maintenanceId; set => maintenanceId = value; }
        public int AssetId { get => assetId; set => assetId = value; }
        public DateTime MaintenanceDate { get => maintenanceDate; set => maintenanceDate = value; }
        public string Description { get => description; set => description = value; }
        public double Cost { get => cost; set => cost = value; }
    }
}