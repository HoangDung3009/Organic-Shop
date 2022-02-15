using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic_Shop.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organic_Shop.Controllers
{
    
    public class BlogController : Controller
    {
        private readonly OrganicShopContext _context;

        public BlogController(OrganicShopContext context)
        {
            this._context = context;
        }
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var size = 20;
            var tin = _context.TinDangs.AsNoTracking().OrderByDescending(x => x.PostId);

            PagedList<TinDang> list = new(tin, pageNumber, size);

            ViewBag.CurrentPage = pageNumber;
            return View(list);
        }

        
        public async Task<IActionResult> BlogDetail(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var blog = await _context.TinDangs.FirstOrDefaultAsync(x => x.PostId == id);

            if(blog == null)
            {
                return NotFound();
            }

            blog.Views++;
            _context.Update(blog);
            await _context.SaveChangesAsync();

            return View(blog);
        }
    }
}
