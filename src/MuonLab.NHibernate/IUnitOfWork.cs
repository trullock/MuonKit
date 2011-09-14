using System;
using NHibernate;

namespace MuonLab.NHibernate
{
	public interface IUnitOfWork : IDisposable
	{
		ISession Session { get; }
		void Commit();
		void Rollback();
	}
}