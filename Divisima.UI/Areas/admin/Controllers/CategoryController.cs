using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.Areas.admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public class CategoryController : Controller
    {
        IRepository<Category> repoCategory;
        public CategoryController(IRepository<Category> _repoCategory)
        {
            repoCategory = _repoCategory;
        }
        public IActionResult Index()
        {
			// return View(repoCategory.GetAll().OrderByDescending(x => x.ID));
			return View(repoCategory.GetAll().Include(x => x.ParentCategory).OrderByDescending(x => x.ID));
		}

        public IActionResult New()
        {
            CategoryVM categoryVM = new CategoryVM()
            {
                Category = new Category(),
                Categories = repoCategory.GetAll().OrderBy(x => x.Name)
            };
            return View(categoryVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(CategoryVM model)
        {
            if (ModelState.IsValid) // Gelen model doğrulanmışsa
            {
                repoCategory.Add(model.Category);

                return RedirectToAction("Index");
            }
            else return RedirectToAction("New");
        }

        public IActionResult Edit(int id)
        {
            Category category = repoCategory.GetBy(x => x.ID == id);
			CategoryVM categoryVM = new CategoryVM()
			{
				Category = category,
				Categories = repoCategory.GetAll().OrderBy(x => x.Name)
			};
			if (category != null) return View(categoryVM);
            else return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryVM model)
        {
            if (ModelState.IsValid) // Gelen model doğrulanmışsa
            {
                repoCategory.Update(model.Category);

                return RedirectToAction("Index");
            }
            else return RedirectToAction("New");
        }


        public IActionResult Delete(int id)
        {
            Category Category = repoCategory.GetBy(x => x.ID == id);
            if (Category != null) repoCategory.Delete(Category);
            return RedirectToAction("Index");
        }
    }
}
