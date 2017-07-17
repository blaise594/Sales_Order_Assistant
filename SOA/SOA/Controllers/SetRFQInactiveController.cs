using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOA.Data;

namespace SOA.Controllers
{
    [Authorize(Roles = "Manager")]
    public class SetRFQInactiveController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SetRFQInactiveController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int Id)
        {

            var order = _context.Order.FirstOrDefault(f => f.Id == Id);
            if (order == null)
            {
                return RedirectToAction("Index", "RFQManager");
            }
            order.Status += 1;
            _context.SaveChanges();

            return RedirectToAction("Index", "RFQManager");
        }
    }
}