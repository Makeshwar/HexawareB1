using NUnit.Framework;
using DigitalAssetManagement; 
using dao;
using entity;
using myexceptions;
using System;
using NUnit.Framework.Legacy;

namespace DigitalAssetManagement.Tests
{
    [TestFixture]
    public class AssetManagementServiceTests
    {
        private IAssetManagementService service;

        [SetUp]
        public void Setup()
        {
            service = new AssetManagementServiceImpl();
        }

        [Test]
        public void Test_AddAsset_Success()
        {
            Asset asset = new Asset {
                Name = "Test Asset",
                Type = "Laptop",
                SerialNumber = "TEST123456",
                PurchaseDate = DateTime.Now,
                Location = "Chennai",
                Status = "in use",
                OwnerId = 1
            };

            bool result = service.AddAsset(asset);
            ClassicAssert.IsTrue(result, "Asset should be added successfully.");
        }

        [Test]
        public void Test_PerformMaintenance_Success()
        {
            int assetId = 1;
            string date = "2025-04-06";
            string description = "Battery Replacement";
            double cost = 1500;

            bool result = service.PerformMaintenance(assetId, date, description, cost);
            ClassicAssert.IsTrue(result, "Maintenance should be recorded successfully.");
        }

        [Test]
        public void Test_ReserveAsset_Success()
        {
            int assetId = 1;
            int employeeId = 1;
            string reservationDate = "2025-04-06";
            string startDate = "2025-04-07";
            string endDate = "2025-04-10";

            bool result = service.ReserveAsset(assetId, employeeId, reservationDate, startDate, endDate);
            ClassicAssert.IsTrue(result, "Reservation should be successful.");
        }

        [Test]
        public void Test_AddMaintenance_InvalidAsset_ThrowsException()
        {
            int invalidAssetId = 9999;
            string date = "2025-04-06";
            string description = "Invalid Asset";
            double cost = 100;

            ClassicAssert.Throws<AssetNotFoundException>(() =>
            {
                service.PerformMaintenance(invalidAssetId, date, description, cost);
            });
        }

        [Test]
        public void Test_ReserveAsset_InvalidEmployee_ThrowsException()
        {
            int assetId = 1;
            int invalidEmpId = 9999;
            string reservationDate = "2025-04-06";
            string startDate = "2025-04-07";
            string endDate = "2025-04-10";

            ClassicAssert.Throws<AssetNotFoundException>(() =>
            {
                service.ReserveAsset(assetId, invalidEmpId, reservationDate, startDate, endDate);
            });
        }
    }
}