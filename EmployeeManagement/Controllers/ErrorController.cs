using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
	public class ErrorController : Controller
	{
		private readonly ILogger<ErrorController> logger;

		public ErrorController(ILogger<ErrorController> logger)
        {
			this.logger = logger;
		}
        [Route("Error/{statusCode}")]
		public IActionResult HttpStatusCodeHandler(int statusCode)
		{
			var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
			switch (statusCode)
			{
				case 404:
					ViewBag.ErrorMessage = "Sorry, the resource you request could not be found";
					logger.LogWarning($"404 error occured. Path={statusCodeResult.OriginalPath}" + $"and QueryString = {statusCodeResult.OriginalQueryString}");
					//ViewBag.Path = statusCodeResult.OriginalPath;
					//ViewBag.QS = statusCodeResult.OriginalQueryString;
					break;
			}
			return View("NotFound");
		}

		[Route("Error")]
		[AllowAnonymous]
		public IActionResult Error()
		{
			var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
			//ViewBag.ExceptionPath = exceptionDetails.Path;
			//ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
			//ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;

			logger.LogError($"The path {exceptionDetails.Path} threw and exception {exceptionDetails.Error}");

			return View("Error");
		}
	}
}
