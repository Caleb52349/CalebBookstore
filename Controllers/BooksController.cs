using CalebBookstore.Data;
using CalebBookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalebBookstore.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookStore book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong,{ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong invalid model");
            return View(book);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var exist = await _context.Books.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return View(exist);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BookStore book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = await _context.Books.Where(x => x.Id == book.Id).FirstOrDefaultAsync();
                    if (exist != null)
                    {
                        exist.Title = book.Title;
                        exist.Authour = book.Authour;
                        exist.Price = book.Price;
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong,{ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong invalid model");
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var exist = await _context.Books.Where(x => x.Id ==Id).FirstOrDefaultAsync();
            return View(exist);
        }     
        [HttpPost]
        public async Task<IActionResult> Delete(BookStore book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = await _context.Books.Where(x => x.Id == book.Id).FirstOrDefaultAsync();
                    if (exist != null)
                    {
                        _context.Remove(exist);
                    }
                    _context.Remove(exist);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong,{ex.Message}");
                }
            }
                ModelState.AddModelError(string.Empty, $"Something went wrong invalid model");
                return View(book);
            }
        }
    }

