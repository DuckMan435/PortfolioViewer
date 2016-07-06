using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortfolioViewer.Models;

namespace PortfolioViewer.Tests
{
    /// <summary>
    /// Summary description for SecuritiesTest
    /// </summary>
    [TestClass]
    public class SecuritiesTest
    {
        public SecuritiesTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion



        [TestMethod]
        public void CalculateCurrentStockValue()
        {
            StockModel stockModel = new StockModel()
            {
                Id = 1,
                Symbol = "AAPL",
                PurchaseDate = DateTime.UtcNow,
                Quantity = 10

            };

            stockModel.PurchasePrice = stockModel.CurrentPrice;

            Assert.AreEqual(stockModel.Cost + ((stockModel.Dividend / stockModel.CurrentPrice) * stockModel.Quantity), stockModel.CurrentValue);
        }

        [TestMethod]
        public void CalculateCurrentFundValue()
        {
            FundModel fundModel = new FundModel()
            {
                Id = 1,
                Symbol = "VFSTX",
                PurchaseDate = DateTime.UtcNow,
                Quantity = 10,
                Dividend = .5

            };

            fundModel.PurchasePrice = fundModel.CurrentPrice;

            Assert.AreEqual(fundModel.Cost + ((fundModel.Dividend / fundModel.CurrentPrice) * 4 * fundModel.Quantity), fundModel.CurrentValue);
        }

        [TestMethod]
        public void CalculateCurrentBondPrice()
        {
            BondModel bondModel = new BondModel()
            {
                FaceValue = 100,
                BondInterestRate = .5,
                MarketInterestRate = .2,
                PurchaseDate = DateTime.UtcNow,
                MaturityDate = DateTime.UtcNow.AddYears(1)
            };

            double value = bondModel.CurrentBondPrice;

            Assert.AreEqual(125, value);
        }
    }
}
