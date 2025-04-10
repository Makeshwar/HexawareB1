namespace entity
{
    public class Reservation
    {
        private int reservationId;
        private int assetId;
        private int employeeId;
        private DateTime reservationDate;
        private DateTime startDate;
        private DateTime endDate;
        private string status;

        public Reservation() { }

        public Reservation(int reservationId, int assetId, int employeeId, DateTime reservationDate, DateTime startDate, DateTime endDate, string status)
        {
            this.reservationId = reservationId;
            this.assetId = assetId;
            this.employeeId = employeeId;
            this.reservationDate = reservationDate;
            this.startDate = startDate;
            this.endDate = endDate;
            this.status = status;
        }

        public int ReservationId { get => reservationId; set => reservationId = value; }
        public int AssetId { get => assetId; set => assetId = value; }
        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public DateTime ReservationDate { get => reservationDate; set => reservationDate = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public string Status { get => status; set => status = value; }
    }
}