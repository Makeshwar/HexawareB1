namespace entity
{
    public class AssetAllocation
    {
        private int allocationId;

        private int assetId;

        private int employeeId;

        private DateTime allocationDate;

        private DateTime? returnDate;

        public AssetAllocation() { }

        public AssetAllocation(int allocationId, int assetId, int employeeId, DateTime allocationDate, DateTime? returnDate)
        {
            this.allocationId = allocationId;

            this.assetId = assetId;

            this.employeeId = employeeId;

            this.allocationDate = allocationDate;

            this.returnDate = returnDate;
        }

        public int AllocationId {

            get => allocationId; set => allocationId = value;

        }

        public int AssetId {

            get => assetId; set => assetId = value;

        }

        public int EmployeeId

        { get => employeeId; set => employeeId = value;

        }

        public DateTime AllocationDate

        { get => allocationDate; set => allocationDate = value; }

        public DateTime? ReturnDate
        {
            get => returnDate; set => returnDate = value;
        }
}
}