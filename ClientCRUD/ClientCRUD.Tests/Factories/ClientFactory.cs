using ClientCRUD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCRUD.Tests.Factories
{
    public static class ClientFactory
    {
        public static Client NewClient()
        {
            Client client = new Client();

            client.Name = Faker.Name.FullName();
            client.Email = Faker.Internet.Email(client.Name);
            client.Birthday = DateTime.Now.AddYears(-Faker.RandomNumber.Next(10, 20));

            return client;
        }
    }
}
