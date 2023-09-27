using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.Areas.admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Divisima.UI.Areas.admin.Controllers
{
    #region validate.min.js içerisinde yapılacak değişiklik
    // (?:,\d{3})+)? (?:\.\d +)?$/.test(a) 
    // arat ve o kısmı 
    // (?:.\d{3})+)? (?:\,\d +)?$/.test(a)
    // olarak değiştir
    // Bu işlem validate.min.js dosyasında yapılacak
    #endregion
    [Area("admin"),Authorize]
    public class ProductController : Controller
    {
        IRepository<Product> repoProduct;
        IRepository<Brand> repoBrand;
        IRepository<Category> repoCategory;
        public ProductController(IRepository<Product> _repoProduct, IRepository<Brand> _repoBrand, IRepository<Category> _repoCategory)
        {
            repoProduct = _repoProduct;
            repoBrand = _repoBrand;
            repoCategory = _repoCategory;
        }
        public IActionResult Index()
        {
            return View(repoProduct.GetAll().OrderByDescending(x => x.ID));
        }

        public IActionResult New()
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                Brands = repoBrand.GetAll().OrderBy(x => x.Name),
                Categories = repoCategory.GetAll(x => x.ParentID!=null).OrderBy(x => x.Name)
            };
            return View(productVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(ProductVM model)
        {
            if (ModelState.IsValid) // Gelen model doğrulanmışsa
            {
                repoProduct.Add(model.Product);

                return RedirectToAction("Index");
            }
            else return RedirectToAction("New");
        }

        public IActionResult Edit(int id)
        {
            Product product = repoProduct.GetBy(x => x.ID == id);
            ProductVM productVM = new ProductVM()
            {
                Product = product,
                Brands = repoBrand.GetAll().OrderBy(x => x.Name),
                Categories = repoCategory.GetAll(x => x.ParentID != null).OrderBy(x => x.Name)
            };
            if (product != null) return View(productVM);
            else return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVM model)
        {
            if (ModelState.IsValid) // Gelen model doğrulanmışsa
            {
				repoProduct.Update(model.Product);

                return RedirectToAction("Index");
            }
            else return RedirectToAction("New");
        }


        public IActionResult Delete(int id)
        {
            Product slide = repoProduct.GetBy(x => x.ID == id);
            if (slide != null) repoProduct.Delete(slide);
            return RedirectToAction("Index");
        }
    }
}
