using System;
using System.Collections.Generic;
using System.Linq;
using MuonKit.Examples.Domain.Entities;
using MuonLab.NHibernate;
using NHibernate.Linq;

namespace MuonKit.Examples.Domain.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork unitOfWork;

		/// <summary>
		/// Creates a new Person Service
		/// </summary>
		/// <param name="unitOfWork">The unitOfWork to use for data operations</param>
		public UserService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public IEnumerable<User> All()
		{
			// Example of Linq to NH
			return this.unitOfWork.Session
				.Linq<User>()
				.OrderBy(p => p.Name);
		}

		public User Get(Guid id)
		{
			// Example of HQL
			return this.unitOfWork.Session
				.CreateQuery("from User where Id=:id")
				.SetParameter("id", id)
				.UniqueResult<User>();
		}

		public void Save(User user)
		{
			unitOfWork.Session.Save(user);
		}
	}
}