using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
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
    public class ProductPictureController : Controller
    {
        IRepository<ProductPicture> repoProductPicture;
        public ProductPictureController(IRepository<ProductPicture> _repoProductPicture)
        {
            repoProductPicture = _repoProductPicture;
        }
        public IActionResult Index(int productid)
        {
            ViewBag.ProductId = productid;

            return View(repoProductPicture.GetAll(x => x.ProductID == productid).OrderByDescending(x => x.ID));
        }

        public IActionResult New(int productid)
        {
            ViewBag.ProductId = productid;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(ProductPicture model)
        {
            if (ModelState.IsValid) // Gelen model doğrulanmışsa
            {
                if (Request.Form.Files.Any())
                {
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture")))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture"));
                    }
                    string dosyaAdi = Request.Form.Files["Picture"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture", dosyaAdi), FileMode.Create))
                    {
                        await Request.Form.Files["Picture"].CopyToAsync(stream);

                    }
                    model.Picture = "/img/productPicture/" + dosyaAdi;
                }
                repoProductPicture.Add(model);

                return RedirectToAction("Index", new { productid = model.ProductID});
            }
            else return RedirectToAction("New");
        }

        public IActionResult Edit(int id)
        {
            ProductPicture productPicture = repoProductPicture.GetBy(x => x.ID == id);
            if (productPicture != null) return View(productPicture);
            else return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductPicture model)
        {
            if (ModelState.IsValid) // Gelen model doğrulanmışsa
            {
				if (Request.Form.Files.Any())
				{
					if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide")))
					{
						Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide"));
					}
					string dosyaAdi = Request.Form.Files["Picture"].FileName;
					using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide", dosyaAdi), FileMode.Create))
					{
						await Request.Form.Files["Picture"].CopyToAsync(stream);
					}
					model.Picture = "/img/slide/" + dosyaAdi;
				}
				repoProductPicture.Update(model);

                return RedirectToAction("Index", new { productid = model.ProductID });
            }
            else return RedirectToAction("New");
        }


        public IActionResult Delete(int id)
        {
            ProductPicture slide = repoProductPicture.GetBy(x => x.ID == id);
            if (slide != null) repoProductPicture.Delete(slide);
            return RedirectToAction("Index");
        }
    }
}
