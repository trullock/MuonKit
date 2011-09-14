using System;

namespace MuonLab.NHibernate
{
	public interface IEntity
	{
		Guid Id { get; }
	}
}