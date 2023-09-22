public interface ICustomer
{
    string Id { get; set; }
    string Name { get; set; }
    // Other properties...
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    // Other address properties...
}

public class AddressIT
{
    public string Street { get; set; }
    public string City { get; set; }
    
}

public class CustomerBase : ICustomer
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    

    
}

public class Customer_GB<TAddress> : CustomerBase where TAddress : Address
{
    public string Country { get; set; }
    public TAddress CustomerAddress { get; set; }
    
}

public class Customer_IT<TAddress> : CustomerBase where TAddress : AddressIT
{
    public string Country { get; set; }
    public TAddress CustomerAddress1 { get; set; }
    public TAddress CustomerAddress2 { get; set; }
    
}
