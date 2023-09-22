namespace Training.Models
{


    public interface ICustomer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }

    }
    public interface ICustomer_GB<TAddress> : ICustomer where TAddress : IAddress
    {
        public TAddress Address { get; set; }
    }
    public interface ICustomer_IT<TAddress> : ICustomer where TAddress : IAddressIT
    {
        public TAddress Address { get; set; }
    }
    public interface IAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }
    public interface IAddressIT : IAddress
    {
      public string AddressLine3 { get; set; }
    }

}