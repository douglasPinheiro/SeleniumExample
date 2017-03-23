using ClientCRUD.Domain.Models;
using ClientCRUD.Tests.Factories;
using ClientCRUD.Tests.Integration.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCRUD.Tests.Integration
{
    [TestClass]
    public class ClientTests
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            string chromeDriverPath = System.IO.Directory.GetCurrentDirectory();
            this.Driver = new ChromeDriver(chromeDriverPath);
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(30));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.Driver.Quit();
        }

        [TestMethod]
        public void Clients_IShouldCreateAClient()
        {
            // prepare
            CreateClientPage createClientPage = new CreateClientPage(this.Driver);
            Client client = ClientFactory.NewClient();

            createClientPage.Navigate();
            createClientPage.FillFields(client);
            createClientPage.ValidateIfWasCreated(client);
        }

        [TestMethod]
        public void Clients_IShouldEditAClient()
        {
            // prepare
            Driver.Navigate().GoToUrl("http://localhost:57226/");

            EditClientPage editClientPage = new EditClientPage(this.Driver);
            Client clienOld = this.GetRandomClient();
            Client clientNew = ClientFactory.NewClient();
            clientNew.Id = clienOld.Id;

            editClientPage.Navigate(clientNew.Id);
            editClientPage.FillFields(clientNew);

            Client clientUpdated = this.GetClientById(clienOld.Id);

            editClientPage.ValidateIfWasUpdated(clientNew, clientUpdated);
        }

        private Client GetRandomClient()
        {
            Client client = new Client();

            int rowsCount = this.Driver.FindElements(By.CssSelector("tbody > tr")).Count() - 1;
            int rowIndex = Faker.RandomNumber.Next(0, rowsCount);
            var row = this.Driver.FindElements(By.CssSelector("tbody > tr")).ElementAt(rowIndex);

            client.Id = Convert.ToInt32(row.FindElement(By.CssSelector(".td-id")).Text);
            client.Name = row.FindElement(By.CssSelector(".td-name")).Text;
            client.Email = row.FindElement(By.CssSelector(".td-email")).Text;
            client.Birthday = Convert.ToDateTime(row.FindElement(By.CssSelector(".td-birthday")).Text);

            return client;
        }

        private Client GetClientById(int id)
        {
            Client client = new Client();

            var rows = this.Driver.FindElements(By.CssSelector("tbody > tr"));

            foreach (var row in rows)
            {
                if (Convert.ToInt32(row.FindElement(By.CssSelector(".td-id")).Text) == id)
                {
                    client.Id = Convert.ToInt32(row.FindElement(By.CssSelector(".td-id")).Text);
                    client.Name = row.FindElement(By.CssSelector(".td-name")).Text;
                    client.Email = row.FindElement(By.CssSelector(".td-email")).Text;
                    client.Birthday = Convert.ToDateTime(row.FindElement(By.CssSelector(".td-birthday")).Text);
                }
            }
            
            return client;
        }
    }
}
