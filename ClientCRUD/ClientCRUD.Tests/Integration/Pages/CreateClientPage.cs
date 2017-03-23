using ClientCRUD.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCRUD.Tests.Integration.Pages
{
    public class CreateClientPage
    {
        private readonly IWebDriver driver;
        private readonly string url = @"http://localhost:57226/Clients/Create";

        public CreateClientPage(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Id, Using = "Name")]
        public IWebElement FieldName { get; set; }

        [FindsBy(How = How.Id, Using = "Email")]
        public IWebElement FieldEmail { get; set; }

        [FindsBy(How = How.Id, Using = "BirthDay")]
        public IWebElement FieldBirthday { get; set; }

        [FindsBy(How = How.Id, Using = "submit")]
        public IWebElement SubmitButton { get; set; }

        public void Navigate()
        {
            this.driver.Navigate().GoToUrl(this.url);
        }

        public void FillFields(Client client)
        {
            this.FieldName.SendKeys(client.Name);
            this.FieldEmail.SendKeys(client.Email);
            this.FieldBirthday.SendKeys(client.Birthday.ToShortDateString());

            this.SubmitButton.Click();
        }
        public void ValidateIfWasCreated(Client client)
        {
            var row = this.driver.FindElements(By.CssSelector("tbody > tr")).Last();
            
            Assert.AreEqual(client.Name, row.FindElement(By.CssSelector(".td-name")).Text, "Name is not equal");
            Assert.AreEqual(client.Email, row.FindElement(By.CssSelector(".td-email")).Text, "Email is not equal");
            Assert.AreEqual(client.Birthday.ToShortDateString(), row.FindElement(By.CssSelector(".td-birthday")).Text, "Birthday is not equal");
        }
    }
}
