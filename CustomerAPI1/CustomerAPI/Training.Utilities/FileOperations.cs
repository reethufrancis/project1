using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Training.Interfaces;
using Training.Models;

namespace Training.Utilities
{
    public class FileOperations
    {
        private readonly string _filePath = "Customer.json";

        public List<ICustomer> ReadCustomerEntities()
        {
            List<ICustomer> items = new List<ICustomer>();
            if (File.Exists(_filePath))
            {
                using (StreamReader r = new StreamReader(_filePath))
                {
                    string json = r.ReadToEnd();
                    List<object> objects = JsonSerializer.Deserialize<List<object>>(json);
                    
                    foreach(var obj in objects)
                    {
                        Customer customer = JsonSerializer.Deserialize<Customer>(obj.ToString());
                        if(customer != null && customer.Country == "IT")
                        {
                            var customerIT = JsonSerializer.Deserialize<Customer_IT<AddressIT>>(obj.ToString());
                            items.Add(customerIT);
                        }
                        else
                        {
                            var customerGB = JsonSerializer.Deserialize<Customer_GB<Address>>(obj.ToString());
                            items.Add(customerGB);
                        }
                    }
                }
            }
            return items;
        }

        public async Task<List<ICustomer>> WriteCustomerEntities(List<ICustomer> items)
        {
            if (items.Any())
            {
                List<object> objects = new List<object>();
                foreach(var item in items)
                {
                    objects.Add(item);
                }
                string json = JsonSerializer.Serialize(objects);
                await File.WriteAllTextAsync(_filePath, json);
            }
            
            return ReadCustomerEntities();
        }

        public async Task<string> DeleteCustomer(string customerId)
        {
            List<ICustomer> items = ReadCustomerEntities();
            if (items.Any())
            {
                items = items.Where(c => c.Id != customerId).ToList();
                _ = await WriteCustomerEntities(items);
            }
            return customerId;
        }
    }
}
