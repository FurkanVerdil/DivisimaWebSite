using Divisima.DAL.Entities;
using Divisima.UI.Models;

namespace Divisima.UI.ViewModels
{
    public class CheckOutVM
    {
        public Order Order{ get; set; }
        public IEnumerable<Cart> Carts{ get; set; }
    }
}
