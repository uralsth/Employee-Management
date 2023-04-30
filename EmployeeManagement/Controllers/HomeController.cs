using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    //public class HomeController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //}

    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployeee();
            return View(model);
        }

        public ViewResult Details()
        {

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(1),
                PageTitle = "Employee Details"
            };

            //Employee model = _employeeRepository.GetEmployee(1);

            //ViewData["Employee"] = model;
            //ViewData["PageTitle"] = "Employee Details";

            //ViewBag.Employee = model;
            //ViewBag.PageTitle = "Employee Details";

            return View(homeDetailsViewModel);

            //return View("Test");
            //return View("MyViews/Tes.cshtml");
        }
        

    }

}
