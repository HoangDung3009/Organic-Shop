using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organic_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {

        private readonly OrganicShopContext _context;

        public SearchController(OrganicShopContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SearchProduct(string keyword)
        {
            List<Product> ls = new List<Product>();

            if(string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("SearchProductPartial", null);
            }

            ls = _context.Products
                                        .AsNoTracking()
                                        .Include(x => x.Cat)
                                        .Where(x => x.ProductName.Contains(keyword))
                                        .OrderByDescending(x => x.ProductId)
                                        .Take(10)
                                        .ToList();

            if(ls == null)
            {
                return PartialView("SearchProductPartial", null);
            }
            else
            {
                return PartialView("SearchProductPartial", ls);
            }
        }
    }
}
