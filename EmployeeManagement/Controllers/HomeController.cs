using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Migrations;

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
		private readonly IWebHostEnvironment webHostEnvironment;

		public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
		{
			_employeeRepository = employeeRepository;
			this.webHostEnvironment = webHostEnvironment;
		}
		public ViewResult Index()
		{
			var model = _employeeRepository.GetAllEmployeee();
			return View(model);
		}

		public ViewResult Details(int? id)
		{

			//throw new Exception("Excetpiton error");
			Employee employee = _employeeRepository.GetEmployee(id.Value);

			if(employee == null)
			{
				Response.StatusCode = 404;
				return View("EmployeeNotFound", id.Value);
			}

			HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
			{
				Employee = employee,
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

		[HttpGet]
		public ViewResult Create()
		{
			return View();
		}

		[HttpGet]
		public ViewResult Edit(int id)
		{
			Employee employee = _employeeRepository.GetEmployee(id);
			EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
			{
				Id = employee.Id,
				Name = employee.Name,
				Email = employee.Email,
				Department = employee.Department,
				ExistingPhotoPath = employee.PhotoPath
			};
			return View(employeeEditViewModel);
		}

		[HttpPost]
		public IActionResult Edit(EmployeeEditViewModel model)
		{
			if (ModelState.IsValid)
			{
				Employee employee = _employeeRepository.GetEmployee(model.Id);
				employee.Name = model.Name;
				employee.Email = model.Email;
				employee.Department = model.Department;
				if (model.Photos.Count > 0 && model.Photos != null)
				{
					if(model.ExistingPhotoPath != null)
					{
						string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
						System.IO.File.Delete(filePath);
					}

					employee.PhotoPath = ProcessUploadedFile(model);
				}

				_employeeRepository.Update(employee);
				return RedirectToAction("index");
			}
			return View();
		}

		private string ProcessUploadedFile(EmployeeCreateViewModel model)
		{
			string uniqueFileName = null;
			if (model.Photos != null && model.Photos.Count > 0)
			{
				foreach (IFormFile photo in model.Photos)
				{
					string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
					uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
					string filePath = Path.Combine(uploadsFolder, uniqueFileName);
					using (var filestream = new FileStream(filePath, FileMode.Create))
					{
						photo.CopyTo(filestream);
					}
				}

			}

			return uniqueFileName;
		}

		[HttpPost]
		public IActionResult Create(EmployeeCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				string uniqueFileName = ProcessUploadedFile(model);
				Employee newEmployee = new Employee
				{
					Name = model.Name,
					Email = model.Email,
					Department = model.Department,
					PhotoPath = uniqueFileName,
				};
				_employeeRepository.Add(newEmployee);
				return RedirectToAction("details", new { id = newEmployee.Id });
			}
			return View();
		}

	}

}
