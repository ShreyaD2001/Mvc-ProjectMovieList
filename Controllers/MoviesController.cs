using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using New_Project.Data;

namespace New_Project.Controllers
{
    public class MoviesController : Controller
    {
        private readonly New_ProjectContext _context;
        private readonly string _uploadsFolder;

        public MoviesController(New_ProjectContext context)
        {
            _context = context;
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        }

        // GET: Movies
        public async Task<IActionResult> Index(string searchString)
        {
            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.Title.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            var movieList = await movies.ToListAsync();
            return View(movieList);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewBag.GenreList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Action", Text = "Action" },
                new SelectListItem { Value = "Comedy", Text = "Comedy" },
                new SelectListItem { Value = "Drama", Text = "Drama" },
                new SelectListItem { Value = "Horror", Text = "Horror" },
                new SelectListItem { Value = "Romance", Text = "Romance" },
                new SelectListItem { Value = "Sci-Fi", Text = "Sci-Fi" },
                new SelectListItem { Value = "Thriller", Text = "Thriller" }
            };

            return View();
        }

        // POST: Movies/Create
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,ProfileFile")] Movies movie)
        {
            if (ModelState.IsValid)
            {*//*
                // Handle the Base64 image data
                if (!string.IsNullOrEmpty(movie.ProfileFile))
                {
                    var base64Data = movie.ProfileFile.Split(',')[1];
                    var imageData = Convert.FromBase64String(base64Data);
                    var uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                    var filePath = Path.Combine(_uploadsFolder, uniqueFileName);

                    System.IO.File.WriteAllBytes(filePath, imageData);

                    movie.ProfileFile = uniqueFileName; // Save the unique file name to the database
                }*//*

                // Add movie to the database
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // If model state is not valid, return to the Create view
            ViewBag.GenreList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Action", Text = "Action" },
                new SelectListItem { Value = "Comedy", Text = "Comedy" },
                new SelectListItem { Value = "Drama", Text = "Drama" },
                new SelectListItem { Value = "Horror", Text = "Horror" },
                new SelectListItem { Value = "Romance", Text = "Romance" },
                new SelectListItem { Value = "Sci-Fi", Text = "Sci-Fi" },
                new SelectListItem { Value = "Thriller", Text = "Thriller" }
            };

            return View(movie);
        }*/

        [HttpPost]
        public IActionResult Create(Movies movie)
        {
            if (ModelState.IsValid)
            {
                // Access the Base64 string from the ProfileFile property
                string base64Image = movie.ProfileFile;

                // Perform any necessary processing with base64Image

                // Save the movie to the database or perform other actions
                _context.Add(movie);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something failed, redisplay form
            return View(movie);
        }



        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,ProfileFile")] Movies movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
