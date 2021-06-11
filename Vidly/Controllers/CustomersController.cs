using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        private VidlyDbContext _context;
        public CustomersController()
        {
            _context = new VidlyDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult Index()
        {

            return View(_context.Customers.Include(c => c.MembershipType).ToList());
        }
        public ActionResult Details(int id)
        {
            return View(_context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id));
        }
        public ActionResult Form()
        {
            var Types = _context.MembershipTypes.ToList();
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = Types
            };
            return View(customerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                CustomerViewModel viewModel = new CustomerViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("Form",viewModel);
            }
            if(customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                Customer customer1 = _context.Customers.Find(customer.Id);
                customer1.Name = customer.Name;
                customer1.IsSubscribed = customer.IsSubscribed;
                customer1.DateOfBirth = customer.DateOfBirth;
                customer1.MembershipTypeId = customer.MembershipTypeId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel viewModel = new CustomerViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("Form",viewModel);
        }
    }
}