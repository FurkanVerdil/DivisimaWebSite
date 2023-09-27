using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.Models;
using Divisima.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Divisima.UI.Controllers
{
	public class CartController : Controller
	{
		IRepository<Product> repoProduct;
		IRepository<Order> repoOrder;
		IRepository<OrderDetail> repoOrderDetail;
        public CartController(IRepository<Product> _repoProduct, IRepository<Order> _repoOrder, IRepository<OrderDetail> _repoOrderDetail)
        {
            repoProduct = _repoProduct;
			repoOrder = _repoOrder;
			repoOrderDetail = _repoOrderDetail;
        }
		[Route("/sepet/sepeteekle"), HttpPost]
		public string AddCart(int productid, int quantity)
		{
			Product product = repoProduct.GetAll(x => x.ID == productid).Include(x => x.ProductPictures).FirstOrDefault();
			bool varMi = false;
			if (product != null)
			{
				Cart cart = new Cart()
				{
					ProductID = productid,
					ProductName = product.Name,
					ProductPicture = product.ProductPictures.FirstOrDefault().Picture,
					ProductPrice = product.Price,
					Quantity = quantity
				};
				List<Cart> carts = new List<Cart>();
				if (Request.Cookies["MyCart"] != null) // daha önce sepete bir ürün eklenmiş ise
				{
					carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
					foreach (Cart c in carts)
					{
						if (c.ProductID == productid)
						{
							varMi = true;
							if (c.ProductID == productid) c.Quantity += quantity;
							break;
 						}
					}
				}
				if (!varMi)
				carts.Add(cart);
				CookieOptions options = new();
				options.Expires = DateTime.Now.AddHours(3);
				Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), options);
				return product.Name;
			}

			// Serialize metodu verilen türü stringe dönüştürür. Yani Json formatına
			// Deserialize ise verilen bir stringi istediğim bir türe dönüştürmek için
			return "~ Ürün Bulunamadı";
		}

		[Route("/sepet/sepetsayisi")]
		public int CartCount()
		{
			int geri = 0;
			if (Request.Cookies["MyCart"] != null) // daha önce sepete bir ürün eklenmiş ise
			{
				List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
				geri = carts.Sum(x => x.Quantity);
			}
			return geri;
		}

        [Route("/sepet/alisverisitamamla")]
        public IActionResult Checkout()
		{
			ViewBag.ShippingFee = 1000;
            if (Request.Cookies["MyCart"] != null)
            {
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
				CheckOutVM checkOutVM = new CheckOutVM
				{
					Order = new Order(),
					Carts = carts
				};
                return View(checkOutVM);
            }else
            return Redirect("/");
        }

        [Route("/sepet/alisverisitamamla"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckOut(CheckOutVM model)
        {
			if (model.Order.PaymentOption == EPaymentOption.KrediKartı) // Kredi Kartı Seçiiliyse
			{
				// Kredi Kartı Kontrol
			}


			model.Order.RecDate = DateTime.Now;
			string orderNumber = DateTime.Now.Microsecond.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Microsecond.ToString() + DateTime.Now.Microsecond.ToString();
			if (orderNumber.Length > 20) orderNumber = orderNumber.Substring(0, 20);
			model.Order.OrderNumber = orderNumber;
			model.Order.OrderStatus = EOrderStatus.Hazırlanıyor;
			repoOrder.Add(model.Order);
			List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
			foreach (Cart cart in carts)
			{
				OrderDetail od = new OrderDetail
				{
					OrderID = model.Order.ID,
					ProductID = cart.ProductID,
					ProductName = cart.ProductName,
					ProductPicture = cart.ProductPicture,
					ProductPrice = cart.ProductPrice,
					Quantity = cart.Quantity
				};
				repoOrderDetail.Add(od);
			}


			return Redirect("/");
        }


        [Route("/sepet")]
        public IActionResult Index()
		{
			if (Request.Cookies["MyCart"] != null)
			{
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
				return View(carts);
            }
			return Redirect("/");
		}
	}
}
