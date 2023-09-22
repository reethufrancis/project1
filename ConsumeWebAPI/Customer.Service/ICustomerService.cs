namespace Customer.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<Model.Customer>> GetAllCustomer();
        Task<Model.Customer> CreateCustomer(Model.Customer customer, string countrycode);
        Task<Model.Customer> GetCustomerById(string id);
        Task<Model.Customer> UpdateCustomer(Model.Customer customer);

        
        Task<string> DeleteCustomer(string id);
        Task GetCustomerByIdAsync(string id);
        Task DeleteCustomerAsync(object existingCustomer);
        Task CreateCustomerAsync(ICustomer customer);
    }
}
    

