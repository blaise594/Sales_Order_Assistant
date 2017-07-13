using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOA.Data;

namespace SOA.Controllers
{
    public class BuyNowController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BuyNowController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int Id)
        {

            var order = _context.Order.FirstOrDefault(f => f.Id == Id);
            if (order == null)
            {
                return RedirectToAction("Index", "Quote");
            }
            order.Status += 3;            
            _context.SaveChanges();
          
            return RedirectToAction("Index", "PlaceOrder");
        }
    }
}