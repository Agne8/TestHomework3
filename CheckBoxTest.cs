using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3Test
{
    class CheckBoxTest
    {

        private static CheckBoxPage _page;

        [OneTimeSetUp]
        public static void SetUp()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            _page = new CheckBoxPage(driver);
        }

        [Order(1)] //anotacija Order, tai testai bus vykdomi is eiles; testai priklausomi vienas nuo kito, jei 1mas fail, tai ir kiti fail
        [Test]

        public static void SingleCheckBoxTest()
        {
            _page.CheckSingleCheckBox()
            .AssertSingleCheckBoxDemoSuccessMessage()
            .UnCheckSingleCheckBox();
        }

        [Order(2)]
        [Test]

        public static void MultipleCheckBoxTest()
        {
            _page.CheckAllMultipleCheckBoxes()
                .AssertButtonName("Uncheck All");
        }

        [Order(3)]
        [Test]
        public static void UncheckMultipleCheckBoxesTest()
        {
            _page.CheckAllMultipleCheckBoxes()
                .ClickGroupButton()
                .AssertMultipleCheckBoxesUnchecked();
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            _page.CloseBrowser();
        }
    }
}
