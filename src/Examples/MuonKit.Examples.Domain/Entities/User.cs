using System;
using MuonKit.Examples.Domain.Enums;
using MuonLab.NHibernate;

namespace MuonKit.Examples.Domain.Entities
{
	public class User : IEntity
	{
		public Guid Id { get; protected set; }

		public string Name { get; set; }
		public string Email { get; set; }
		public int Age { get; set; }
		public Gender Gender { get; set; }
	}
}