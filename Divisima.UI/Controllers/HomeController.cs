using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Divisima.UI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Slide> repoSlide;
        IRepository<Product> repoProduct;
        public HomeController(IRepository<Slide> _repoSlide, IRepository<Product> _repoProduct)
        {
            repoSlide = _repoSlide;
            repoProduct = _repoProduct;
        }
        public IActionResult Index()
        {
            IndexVM indexVM = new IndexVM()
            {
                Slides = repoSlide.GetAll().OrderBy(x => x.DisplayIndex),
                Products = repoProduct.GetAll().Include(x => x.ProductPictures).OrderByDescending(x => x.ID).Take(8)
            };
            return View(indexVM);
        }
    }
}
