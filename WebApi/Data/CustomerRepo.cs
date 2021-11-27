using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public class CustomerRepo : ICustomerRepo
    {

        private readonly CustomerContext _context;

        public CustomerRepo(CustomerContext context)
        {
            _context = context;
        }

        public async Task CreateCustomerAsync(Customer cus)
        {
            if (cus == null)
            {
                throw new ArgumentNullException(nameof(cus));
            }

            await _context.Customers.AddAsync(cus);
        }
        
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync<Customer>();
        }

        public async Task<Customer> GetCustomerByIdAsync(long id)
        {
            return await _context.Customers.FirstOrDefaultAsync(p => p.Id == id);
        }
             

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
