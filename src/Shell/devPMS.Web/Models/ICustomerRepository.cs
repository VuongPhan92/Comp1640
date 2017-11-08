using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.Web.Models
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
    }

    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer{Id=1,FirstName = "Jalpesh", LastName = "Vadgama"},
                new Customer{Id=2,FirstName = "Vishal",LastName = "Vadgama"}
            };
            return customers;
        }
    }
}
