using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Divisima.UI.Controllers
{
	public class ProductController : Controller
	{
		IRepository<Product> repoProduct;
		public ProductController(IRepository<Product> _repoProduct)
		{
			repoProduct = _repoProduct;
		}


		public IActionResult Index()
		{
			return View();
		}

		[Route("urun/{name}-{id}")]
		public IActionResult Detail(string name, int id)
		{
			Product product = repoProduct.GetAll(x => x.ID == id).Include(x => x.Category).Include(x => x.ProductPictures).FirstOrDefault();

			if (product != null)
			{
				ProductVM productVM = new ProductVM()
				{
					Product = product,
					RelatedProducts = repoProduct.GetAll(x => x.ID == product.CategoryID && x.ID != product.ID).Include(x => x.ProductPictures)
				};
				return View(productVM);
			}
			else
			{
				return Redirect("/");
			}
		}
	}
}
