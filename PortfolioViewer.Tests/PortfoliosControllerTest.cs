using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortfolioViewer.Controllers;
using PortfolioViewer.Models;
using System.Linq;
using Moq;
using System.Data.Entity;

namespace PortfolioViewer.Tests
{
    /// <summary>
    /// Summary description for PortfoliosControllerTest
    /// </summary>
    [TestClass]
    public class PortfoliosControllerTest
    {
        public PortfoliosControllerTest()
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
        public void GetAllPortfolios()
        {
            var data = new List<PortfolioModel>()
            {
                new PortfolioModel()
                {
                    Name = "Client 1",
                    UserName = "Client 1",
                    Securities = new List<SecurityModel>()
                            {
                                new StockModel() { Symbol = "AAPL", PurchasePrice = 105.35, Quantity = 500, Type = SecurityType.Stock, PurchaseDate = new DateTime(2016, 1, 4) },
                                new FundModel() { Symbol = "VFSTX", PurchasePrice = 10.56, Quantity = 1000, Type = SecurityType.Fund, PurchaseDate = new DateTime(2016, 1, 4), FundDividend = .25 },
                                new BondModel() { Name = "U.S. $1000 10 Year", Type = SecurityType.Bond, PurchaseDate = new DateTime(2016, 1, 4), MaturityDate = new DateTime(2026, 1, 4),
                                                      FaceValue = 1000, BondInterestRate = .10, MarketInterestRate = .02 },
                            }
                },
                new PortfolioModel()
                {
                    Name = "Client 2",
                    UserName = "Client 2",
                    Securities = new List<SecurityModel>()
                            {
                                new StockModel() { Symbol = "GOOG", PurchasePrice = 741.84, Quantity = 500, Type = SecurityType.Stock, PurchaseDate = new DateTime(2016, 1, 4) },
                                new FundModel() { Symbol = "VGSTX", PurchasePrice = 23.05, Quantity = 1000, Type = SecurityType.Fund, PurchaseDate = new DateTime(2016, 1, 4), FundDividend = .50 },
                                new BondModel() { Name = "U.S. $1000 20 Year", Type = SecurityType.Bond, PurchaseDate = new DateTime(2016, 1, 4), MaturityDate = new DateTime(2036, 1, 4),
                                                      FaceValue = 1000, BondInterestRate = .05, MarketInterestRate = .02 },
                            }
                },
                new PortfolioModel()
                {
                    Name = "Client 3",
                    UserName = "Client 3",
                    Securities = new List<SecurityModel>()
                            {
                                new StockModel() { Symbol = "MSFT", PurchasePrice = 54.80,  Quantity = 1500, Type = SecurityType.Stock, PurchaseDate = new DateTime(2016, 1, 4) },
                                new FundModel() { Symbol = "VTHRX", PurchasePrice = 27.39, Quantity = 2000, Type = SecurityType.Fund, PurchaseDate = new DateTime(2016, 1, 4), FundDividend = .75 },
                                new BondModel() { Name = "U.S. $1000 30 Year", Type = SecurityType.Bond, PurchaseDate = new DateTime(2016, 1, 4), MaturityDate = new DateTime(2046, 1, 4),
                                                      FaceValue = 1000, BondInterestRate = .15, MarketInterestRate = .02 },
                            }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<PortfolioModel>>();
            mockSet.As<IQueryable<PortfolioModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<PortfolioModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<PortfolioModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<PortfolioModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PortfolioViewerContext>();
            mockContext.Setup(c => c.Portfolios.Include("Securities")).Returns(mockSet.Object);

            var repo = new Repository(mockContext.Object);
            var controller = new PortfoliosController(repo);
            var portfolios = controller.Get();

            Assert.AreEqual(3, portfolios.Count());
        }
    }
}
