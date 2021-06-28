using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.DTOS;
using AutoMapper;


namespace Vidly.Controllers.api
{
    public class CustomersController : ApiController
    {
        private VidlyDbContext _dbContext;
        public CustomersController()
        {
            _dbContext = new VidlyDbContext();
        }

        //GET /api/Customers
        public IHttpActionResult GetCustomers()
        {
            return Ok(_dbContext.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }

        //GET /api/Customers/id
        public IHttpActionResult GetCustomers(int id)
        {
            Customer customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /api/Customers(Store Customer Details in Database)
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            Customer customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            customerDto.Id = customer.Id;
            return Created( new Uri(Request.RequestUri + "/" + customerDto.Id),customerDto);
        }

        //PUT /api/Customers/Id(update customer details)
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Customer customer = _dbContext.Customers.Find(id);
            if (customer == null)
                return NotFound();
            Mapper.Map<CustomerDto, Customer>(customerDto, customer);
            _dbContext.SaveChanges();
            return Ok(customerDto);
        }
        //DELETE api/Customers/id
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Customer customer = _dbContext.Customers.Find(id);
            if (customer == null)
                return NotFound();
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
            return Ok("Deleted Successfully");
        }
    }
}
