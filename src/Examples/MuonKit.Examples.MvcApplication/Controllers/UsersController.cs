using System.Web.Mvc;
using MuonKit.Examples.Domain.Entities;
using MuonKit.Examples.MvcApplication.Resources.Users;
using MuonLab.Web.Mvc;

namespace MuonKit.Examples.MvcApplication.Controllers
{
	public class UsersController : BaseController<UsersController>
	{
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Add()
		{
			var resource = new AddUserResource();

			return View("Add", resource);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Add(AddUserResource resource)
		{
			if (!ModelState.IsValid)
				return View("Add", resource);

			var user = new User();

			user.Name = resource.Name;
			user.Age = resource.Age;
			user.Gender = resource.Gender;
			user.Email = resource.Email;

			// Todo: save user here

			return Redirect("/somewhere");
		}
	}
}