using System.Data;
using NHibernate;

namespace MuonLab.NHibernate
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ISessionFactory sessionFactory;
		private readonly ITransaction transaction;

		public UnitOfWork(ISessionFactory sessionFactory)
		{
			this.sessionFactory = sessionFactory;
			this.Session = this.sessionFactory.OpenSession();
			this.transaction = this.Session.BeginTransaction(IsolationLevel.ReadCommitted);
		}

		public ISession Session { get; private set; }

		public void Dispose()
		{
			this.Session.Close();
			this.Session = null;
		}

		public void Commit()
		{
			if (this.transaction.IsActive)
				this.transaction.Commit();
		}

		public void Rollback()
		{
			if (this.transaction.IsActive)
				this.transaction.Rollback();
		}
	}
}