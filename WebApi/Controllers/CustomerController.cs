using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Exceptions;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepo _repo;

        [HttpGet("{id:long}")]   
        public async Task<Customer> GetCustomerAsync([FromRoute] long id)
        {
            var findCustomer = await _repo.GetCustomerByIdAsync(id);
            if (findCustomer == null)
            {
                throw new NotFoundException($"Пользователь с id={id}, не найден.");
            }

            return findCustomer;
        }

        [HttpPost("")]   
        public async Task<long> CreateCustomerAsync([FromBody] Customer customer)
        {
            if (customer?.Id != 0)
            {
                var findCustomer = await _repo.GetCustomerByIdAsync(customer.Id);
                if (findCustomer != null)
                {
                    throw new ConflictException($"Нельзя добавить Consumer c Id={customer.Id}, так как такой уже есть.");
                }
            }

            await _repo.CreateCustomerAsync(customer);
            await _repo.SaveChangesAsync();

            return customer.Id;
        }

        public CustomerController(ICustomerRepo repo)
        {
            _repo = repo;
        }
    }
}