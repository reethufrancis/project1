using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Training.Models;
using Training.Interfaces;
using System.Text.Json;
using Training.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Net;

namespace Training.CustomerAPI.Web.Function.Functions
{
    public class CustomerCRUD
    {
        private readonly FileOperations _fileService;

        public CustomerCRUD(FileOperations fileService)
        {
            _fileService = fileService;
        }
        
        [FunctionName("CreateCustomer")]
        public  async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1.0/customer/create/{countrycode}")]
HttpRequest req, ILogger log, string countrycode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            ICustomer customer = null;
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (!string.IsNullOrEmpty(countrycode) && countrycode == "GB")
                {
                    customer = JsonSerializer.Deserialize<Customer_GB<Address>>(requestBody, options);
                }
                else if (!string.IsNullOrEmpty(countrycode) && countrycode == "IT")
                {
                    customer = JsonSerializer.Deserialize<Customer_IT<AddressIT>>(requestBody, options);
                }

                if (customer == null)
                {
                    return new BadRequestObjectResult("Invalid country code or customer data.");
                }

                
                List<ICustomer> customers = _fileService.ReadCustomerEntities();
                customers.Add(customer);

                _ = await _fileService.WriteCustomerEntities(customers);

                return new OkObjectResult(customer);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "An error occurred while processing the request.");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }


        [FunctionName("GetAllCustomers")]
        public IActionResult GetAll(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1.0/get-customer/all")]
        HttpRequest req, ILogger log)
        {
            List<ICustomer> customers = _fileService.ReadCustomerEntities();
            return new OkObjectResult(customers);
        }

        [FunctionName("GetCustomerById")]
        public IActionResult GetById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1.0/get-customer/{id}")]
        HttpRequest req, ILogger log, string id)
        {
            IList<ICustomer> customers = _fileService.ReadCustomerEntities();
            if(customers != null)
            {
                return new OkObjectResult(customers.Where(c => c.Id == id).FirstOrDefault());
            }
            return new NotFoundObjectResult($"No Customer with Id: {id}");
        }
        
    }
}
