using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3Test
{
    public class BasePage
    {
        protected static IWebDriver Driver;

        public BasePage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        public WebDriverWait GetWait(int seconds = 5) //jei nepaduosim, tai bus default 5sek; lauksim 5sek, jei neprasys daugiau
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds)); //"seconds, paduodam laika kaip parametrus
            return wait;
        }

        public void CloseBrowser()
        {
            Driver.Quit();
        }

    }
}



