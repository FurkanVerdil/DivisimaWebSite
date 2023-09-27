using Divisima.DAL.Entities;

namespace Divisima.UI.ViewModels
{
    public class IndexVM
    {
        public IEnumerable<Slide> Slides { get; set; }
        public IEnumerable<Product> Products{ get; set; }

    }
}
