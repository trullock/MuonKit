using System;
using System.Collections.Generic;
using MuonKit.Examples.Domain.Entities;

namespace MuonKit.Examples.Domain.Services
{
	public interface IUserService
	{
		/// <summary>
		/// Get all Users
		/// </summary>
		/// <returns>All Users in alphabetical order by Name</returns>
		IEnumerable<User> All();

		/// <summary>
		/// Gets a single User
		/// </summary>
		/// <param name="Id">The Id of the User to get</param>
		/// <returns></returns>
		User Get(Guid Id);

		/// <summary>
		/// Saves a User
		/// </summary>
		/// <param name="user"></param>
		void Save(User user);
	}
}