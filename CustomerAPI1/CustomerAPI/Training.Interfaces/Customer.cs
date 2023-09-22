using System.Net;
using Training.Models;

namespace Training.Interfaces
{
    public class Customer : ICustomer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public string Age { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;

    }

    public class Customer_GB<TAddress> : Customer, ICustomer_GB<TAddress> where TAddress : IAddress
    {
        public TAddress Address { get; set; }
    }
    public class Customer_IT<TAddress> : Customer, ICustomer_GB<TAddress> where TAddress : IAddressIT
    {
        public TAddress Address { get; set; }
    }

    public class Address : IAddress
    {
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
    }
    public class AddressIT : Address, IAddressIT
    {
        public string AddressLine3 { get; set; } = string.Empty;
    }

    
}