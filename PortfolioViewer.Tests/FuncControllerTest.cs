using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortfolioViewer.Controllers;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;

namespace PortfolioViewer.Tests
{
    /// <summary>
    /// Summary description for FuncControllerTest
    /// </summary>
    [TestClass]
    public class FuncControllerTest
    {
        public FuncControllerTest()
        {
            controller = new FuncController();
        }

        private TestContext testContextInstance;
        private FuncController controller;

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
        public void CalculateCurrentStockGain()
        {
            int quantity = 10;
            double currentPrice = 1;
            double stockDividend = .5;

            double value = controller.CalculateCurrentStockGain(quantity, currentPrice, stockDividend);

            Assert.AreEqual(15, value);
        }

        [TestMethod]
        public void CalculateCurrentFundGain()
        {
            int quantity = 10;
            double currentPrice = 1;
            double fundDividend = .5;

            double value = controller.CalculateCurrentFundGain(quantity, currentPrice, fundDividend);

            Assert.AreEqual(30, value);
        }

        [TestMethod]
        public void CalculateCurrentBondPrice()
        {
            double faceValue = 100;
            double bondInterestRate = .5;
            double marketInterestRate = .2;
            double numberOfPeriods = 1;

            double value = controller.CalculateCurrentBondPrice(faceValue, bondInterestRate, marketInterestRate, numberOfPeriods);

            Assert.AreEqual(125, value);
        }
    }
}
