using Customer.Service.Model;

namespace ConsumeWebAPI.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public class CustomerWithCountry
        {
            public TCustomer Customer { get; set; }
            public string Country { get; set; }
        }


    }
}

