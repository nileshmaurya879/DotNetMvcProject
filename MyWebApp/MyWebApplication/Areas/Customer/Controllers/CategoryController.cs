using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyWebApplication.DataAccessLayer.IRepository;
using MyWebApplication.Models.Model;
using MyWebApplication.Models.ViewModels;
using System.Collections.Generic;

namespace MyWebApplication.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CategoryController : Controller
    {
        // private readonly ApplicationDBContext _applicationDBContext;

        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            _ = _unitOfWork.Category.GetAll();
            return View();
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM categoryVM = new CategoryVM();
            if (id == null)
            {
                return View(categoryVM);
            }
            categoryVM.Category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (categoryVM.Category == null)
            {
                return NotFound();
            }
            return View(categoryVM);
        }
        [HttpPost]
        public IActionResult CreateUpdate(Category category)
        {
            if (ModelState.IsValid)
            {
                if(category.Id == 0)
                    _unitOfWork.Category.Add(category);
                else
                    _unitOfWork.Category.Update(category);

                _unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteData(int? id)
        {
            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Delete(category);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
