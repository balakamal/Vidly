using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        private VidlyDbContext _context;
        public MoviesController()
        {
            _context = new VidlyDbContext();
        }
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            foreach(var i in movies)
                Console.WriteLine(i.Name);
            return View(movies);
        }
        public ActionResult Details(int id)
        {
            return View(_context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id));
        }

        [Route("Movie/Add")]
        public ActionResult Form()
        {
            var genre = _context.Genres.ToList();
            MoviesViewModel viewModel = new MoviesViewModel()
            {
                Movie = new Movie(),
                Genres = genre
            };
            ViewBag.Message = "New Movie";
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                MoviesViewModel viewModel = new MoviesViewModel()
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };
                return View("Form",viewModel);
            }
            if(movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                Movie movie1 = _context.Movies.Find(movie.Id);
                movie1.Name = movie.Name;
                movie1.ReleaseDate = movie.ReleaseDate;
                movie1.AddedDate = movie.AddedDate;
                movie1.InStock = movie.InStock;
                movie1.GenreId = movie.GenreId;
            }
        _context.SaveChanges();
        return RedirectToAction("Index", "Movies");
        }
        public ActionResult Edit(int id)
        {
            MoviesViewModel viewModel = new MoviesViewModel()
            {
                Movie = _context.Movies.Single(m => m.Id == id),
                Genres = _context.Genres.ToList()
            };
            ViewBag.Message = "Edit Movie";
            return View("Form", viewModel);
        }
    }
}