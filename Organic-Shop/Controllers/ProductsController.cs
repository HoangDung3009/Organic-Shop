using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Organic_Shop.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organic_Shop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly OrganicShopContext _context;

        public ProductsController(OrganicShopContext _context)
        {
            this._context = _context; 
        }
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var size = 12;
            var product = _context.Products.Include(c => c.Cat).AsNoTracking().Where(x => x.UnitsInStock > 0 && x.Active).OrderByDescending(x => x.ProductId);

            PagedList<Product> list = new(product, pageNumber, size);

            ViewBag.CurrentPage = pageNumber;

            ViewData["ListCategory"] = new SelectList(_context.Categories, "CatId", "CatName");

            List<SelectListItem> list1 = new List<SelectListItem>();
            list1.Add(new SelectListItem() { Text = "In stock", Value = "1" });
            list1.Add(new SelectListItem() { Text = "Out of stock", Value = "0" });
            ViewData["ProductStatus"] = list1;
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(x => x.Cat).FirstOrDefaultAsync(x => x.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View();
        }
    }
}
