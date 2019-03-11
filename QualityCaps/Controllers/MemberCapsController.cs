using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualityCaps.Data;
using QualityCaps.Models;

namespace QualityCaps.Controllers
{
    public class MemberCapsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public string ShoppingCartId { get; set; }
        public MemberCapsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MemberCaps
        public async Task<IActionResult> Index(string sortOrder,
            string searchString,
            string currentFilter,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var caps = from s in _context.Cap
                       .Include(c => c.Category)
                       .Include(b => b.Supplier)
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                caps = caps.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    caps = caps.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    caps = caps.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    caps = caps.OrderByDescending(s => s.Price);
                    break;
                default:
                    caps = caps.OrderBy(s => s.Name);
                    break;
            }



            int pageSize = 4;
            return View(await PaginatedList<Cap>.CreateAsync(caps.AsNoTracking(), page ?? 1, pageSize));

           // var applicationDbContext = _context.Cap.Include(c => c.Category).Include(c => c.Supplier);
           // return View(await applicationDbContext.ToListAsync());
        }

        public void AddToCart(Cap cap, ApplicationDbContext db)
        {
            var cartItem = db.CartItems.SingleOrDefault(c => c.CartID == ShoppingCartId && c.Cap.ID == cap.ID);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Cap = cap,
                    CartID = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            db.SaveChanges();
        }


        private bool CapExists(int id)
        {
            return _context.Cap.Any(e => e.ID == id);
        }
    }
}
