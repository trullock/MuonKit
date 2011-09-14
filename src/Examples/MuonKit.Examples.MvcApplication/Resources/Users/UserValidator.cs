using MuonLab.Validation;

namespace MuonKit.Examples.MvcApplication.Resources.Users
{
	public class AddUserResourceValidator : Validator<AddUserResource>
	{
		protected override void Rules()
		{
			Ensure(u => u.Name.IsNotNullOrEmpty());

			Ensure(u => u.Email.IsNotNullOrEmpty()).And(() => 
				Ensure(u => u.Email.IsAValidEmailAddress()));

			Ensure(u => u.Age.IsGreaterThan(17));
		}
	}
}