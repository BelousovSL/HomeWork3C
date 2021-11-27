using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public interface ICustomerRepo
    {
        public Task CreateCustomerAsync(Customer cus);

        public Task<IEnumerable<Customer>> GetAllCustomersAsync();

        public Task<Customer> GetCustomerByIdAsync(long id);

        public Task<bool> SaveChangesAsync();
    }
}
