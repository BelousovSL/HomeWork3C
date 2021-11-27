using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace WebClient
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var customerHelper = new CustomerHelper("http://localhost:5000/");

            var select = "";
            do
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("   1) Добавить случайно сгенерированного пользователя");
                Console.WriteLine("   2) Получить пользователя по Id");
                Console.WriteLine("   3) Выход");

                select = Console.ReadLine();

                switch (select)
                {
                    case "1":
                        await customerHelper.PostCustomer(RandomCustomer());
                        break;

                    case "2":
                        await customerHelper.GetCustomerAsync();
                        break;

                    default:
                        Console.WriteLine("Введено не корректное значение");
                        continue;
                }

            }
            while (select != "3");
                      
        }

        private static CustomerCreateRequest RandomCustomer()
        {
            Random rand = new Random();

            var arrayFirstname = new[] { "Сергей", "Николай", "Петр", "Василий", "Игнатий", "Евгений", "Ярослав" };
            var arrayLastname = new[] { "Белоусов", "Кольба", "Слепов", "Пекшев", "Романов", "Егоров" };

            return new CustomerCreateRequest(
                arrayFirstname[rand.Next(0, arrayFirstname.Length - 1)],
                arrayLastname[rand.Next(0, arrayLastname.Length - 1)]);
        }
    }
}