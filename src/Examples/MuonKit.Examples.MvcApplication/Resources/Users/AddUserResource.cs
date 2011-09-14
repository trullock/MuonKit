using MuonKit.Examples.Domain.Enums;

namespace MuonKit.Examples.MvcApplication.Resources.Users
{
	public class AddUserResource
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public int Age { get; set; }
		public Gender Gender { get; set; }
	}
}