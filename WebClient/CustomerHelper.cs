using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebClient
{
    public class CustomerHelper
    {
        private readonly string _curl;

        public async Task GetCustomerAsync()
        {
            Console.WriteLine("Введите Id Customer");
            var select = Console.ReadLine();
            long id = 0;
            if (!long.TryParse(select, out id))
            {
                Console.WriteLine("Введено не корректное значение");
                return;
            }

            var customer = await GetCustomerById(id);
            if (customer == null)
            {
                Console.WriteLine($"Пользователь с id={id} не найден ");
            }
            else
            { 
                Console.WriteLine(customer);
            }
        }

        public async Task PostCustomer(CustomerCreateRequest customerCreateRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_curl);
                var response = client.PostAsJsonAsync("customers", customerCreateRequest).Result;
                if (response.IsSuccessStatusCode)
                {
                   var id = await response.Content.ReadAsStringAsync();
                   Console.WriteLine($"Пользователь успешно добавлен id={id}");
                }
                else
                    Console.WriteLine("Ошибка добавления");
            }
        }

        private async Task<Customer> GetCustomerById(long id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_curl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync($"customers/{id.ToString()}").Result;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await JsonSerializer.DeserializeAsync<Customer>(await response.Content.ReadAsStreamAsync());
                    case HttpStatusCode.NotFound:
                        return null;
                    default:
                        throw new Exception("Ошибка!");
                }
            }
        }


        public CustomerHelper(string curl)
        {
            _curl = curl;
        }
    }
}
