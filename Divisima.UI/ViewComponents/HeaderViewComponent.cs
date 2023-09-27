using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Divisima.UI.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        IRepository<Category> repoCategogory;
        public HeaderViewComponent(IRepository<Category> _repoCategogory)
        {
            repoCategogory = _repoCategogory;
        }
        public IViewComponentResult Invoke()
        {
            return View(repoCategogory.GetAll().Include(x => x.SubCategories).OrderBy(x => x.DisplayIndex));
        }
    }
}
