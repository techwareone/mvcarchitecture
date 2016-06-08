using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Store.Model;
using Store.Service;
using Store.Web.ViewModels;

namespace Store.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IGadgetService gadgetService;

        public HomeController(ICategoryService categoryService, IGadgetService gadgetService)
        {
            this.categoryService = categoryService;
            this.gadgetService = gadgetService;
        }

        // GET: Home
        public ActionResult Index(string category = null)
        {
            IEnumerable<Category> categories = categoryService.GetCategories(category).ToList();
            var viewModelGadgets = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);
            return View(viewModelGadgets);
        }

        public ActionResult Filter(string category, string gadgetName)
        {
            var gadgets = gadgetService.GetCategoryGadgets(category, gadgetName);
            var viewModelGadgets = Mapper.Map<IEnumerable<Gadget>, IEnumerable<GadgetViewModel>>(gadgets);

            return View(viewModelGadgets);
        }

        [HttpPost]
        public ActionResult Create(GadgetFormViewModel newGadget)
        {
            if (newGadget != null && newGadget.File != null)
            {
                var gadget = Mapper.Map<GadgetFormViewModel, Gadget>(newGadget);
                gadgetService.CreateGadget(gadget);

                var gadgetPicture = Path.GetFileName(newGadget.File.FileName);
                var path = Path.Combine(Server.MapPath("~/images/"), gadgetPicture);
                newGadget.File.SaveAs(path);

                gadgetService.SaveGadget();
            }

            var category = categoryService.GetCategory(newGadget.GadgetCategory);
            return RedirectToAction("Index", new {category = category.Name});
        }
    }
}