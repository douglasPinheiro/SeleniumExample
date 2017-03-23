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
    public class EditClientPage
    {
        private readonly IWebDriver driver;
        private readonly string url = @"http://localhost:57226/Clients/Edit/{0}";

        public EditClientPage(IWebDriver browser)
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

        public void Navigate(int id)
        {
            this.driver.Navigate().GoToUrl(string.Format(this.url, id));
        }

        public void FillFields(Client client)
        {
            this.FieldName.Clear();
            this.FieldEmail.Clear();
            this.FieldBirthday.Clear();

            this.FieldName.SendKeys(client.Name);
            this.FieldEmail.SendKeys(client.Email);
            this.FieldBirthday.SendKeys(client.Birthday.ToShortDateString());

            this.SubmitButton.Click();
        }

        public void ValidateIfWasUpdated(Client newClient, Client updatedClient)
        {
            Assert.AreEqual(updatedClient.Name, newClient.Name);
            Assert.AreEqual(updatedClient.Email, newClient.Email);
            Assert.AreEqual(updatedClient.Birthday.ToShortDateString(), newClient.Birthday.ToShortDateString());
        }
    }
}
