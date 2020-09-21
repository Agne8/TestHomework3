using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3Test
{
    public class CheckBoxPage : BasePage
    {

        private const string PageAddress = "http://www.seleniumeasy.com/test/basic-checkbox-demo.html";
        private IWebElement _singleCheckBox => Driver.FindElement(By.Id("isAgeSelected"));
        private IWebElement _singleCheckBoxMessage => Driver.FindElement(By.Id("txtAge"));
        private IReadOnlyCollection<IWebElement> _multipleCheckBoxes => Driver.FindElements(By.ClassName("cb1-element")); //naudojam sarasa, nes 4 tick box'ai yra elementu rinkinys
        private IWebElement _checkAllButton => Driver.FindElement(By.Id("check1"));

        private const string SingleCheckBoxMessageText = "Success - Check box is checked"; // const, tai negalime perrasyti, jis nekinta


        public CheckBoxPage(IWebDriver webdriver) : base(webdriver) // konstruktorius
        {
            Driver.Url = PageAddress;
        }


        public CheckBoxPage CheckSingleCheckBox()
        {
            if (!_singleCheckBox.Selected) //turim pazymeti tik tada, jei jis nera pazymetas     
                _singleCheckBox.Click();
            return this;
        }

        public CheckBoxPage UnCheckSingleCheckBox()
        {
            if (_singleCheckBox.Selected) //atzymi, jei box'as jau pazymetas
                _singleCheckBox.Click();
            return this;
        }

        public CheckBoxPage AssertSingleCheckBoxDemoSuccessMessage() //tirinam ar tokia success zinute
        {
            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); // 5 sek neuzteko ; iskeliam i BasePage
            GetWait().Until(ExpectedConditions.TextToBePresentInElement(_singleCheckBoxMessage, SingleCheckBoxMessageText));

            Assert.AreEqual(SingleCheckBoxMessageText, _singleCheckBoxMessage.Text, "Tekstas nesutampa");
            return this;
        }

        public CheckBoxPage AssertSingleCheckBoxDemoSuccessMessageWithWait()
        {
            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); // 5 sek neuzteko padarem 10; iskelem sita i BasePage(supaprastinom, nes kartojasi)
            GetWait(2).Until(ExpectedConditions.TextToBePresentInElement(_singleCheckBoxMessage, SingleCheckBoxMessageText)); //GetWait ateina is BasePage klases, supaprastintas wait; lauksim 2 sekundes
            Assert.AreEqual(SingleCheckBoxMessageText, _singleCheckBoxMessage.Text, "Tekstas nesutampa");
            return this;
        }

        public CheckBoxPage CheckAllMultipleCheckBoxes()
        {
            foreach (IWebElement singleCheckBox in _multipleCheckBoxes) //foreach dirba su collection'ais, list'ais, eina per juos visus; IWebElement tipas, eis per kiekviena kolekcijos (_multipleCheckBoxes) elementa
            {
                if (!singleCheckBox.Selected) //jei nera selected - paklikinam
                    singleCheckBox.Click();
            }
            return this;
        }

        public CheckBoxPage AssertButtonName(string expectedName)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); // 5 sek neuzteko 
            wait.Until(ExpectedConditions.TextToBePresentInElementValue(_checkAllButton, expectedName));      // norim palaukti kol tekstas atsiras mygtuke; button'as turi atributa value, todel renkames TextToBePresentInElementValue; ExpectedConditions pabraukta zaliai, nes bus ateityje panaikinta, bet kol kas palaikoma?
            Assert.AreEqual(expectedName, _checkAllButton.GetAttribute("value"), "Wrong button label");
            return this;
        }

        public CheckBoxPage ClickGroupButton()
        {
            _checkAllButton.Click();
            return this;
        }

        public CheckBoxPage AssertMultipleCheckBoxesUnchecked()
        {
            foreach (IWebElement singleCheckbox in _multipleCheckBoxes)
            {
                Assert.False(singleCheckbox.Selected, "One of checkboxes is still checked!");
            }
            return this;
        }
    }
}