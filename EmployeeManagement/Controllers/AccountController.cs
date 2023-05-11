using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
	}
}
