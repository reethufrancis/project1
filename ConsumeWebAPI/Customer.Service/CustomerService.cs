using Customer.Service.Model;
//using System.Text.Json;

namespace Customer.Service
{
    public class CustomerService : ICustomerService
    {
        public readonly ICustomerWebService _customerWebService;
        public CustomerService(ICustomerWebService customerWebService)
        {
            _customerWebService = customerWebService;
        }

        public async Task<IEnumerable<Model.Customer>> GetAllCustomer()
        {
            return await _customerWebService.GetAllCustomer();
        }
        public async Task<Model.Customer> CreateCustomer(Model.Customer customer, string countrycode)
        {
            return await _customerWebService.CreateCustomer(customer, countrycode);
        }


    
    public async Task<Model.Customer> GetCustomerById(string id)
        {
            return await _customerWebService.GetCustomerById(id);
        }
        public async Task<Model.Customer> UpdateCustomer(Model.Customer customer)
        {
            return await _customerWebService.UpdateCustomer(customer);
        }

        public async Task<string> DeleteCustomer(string id)
        {
            return await _customerWebService.DeleteCustomer(id);
        }

    }
}

    

    