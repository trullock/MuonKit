using System;
using NUnit.Framework;

namespace MuonLab.Testing
{
	[TestFixture]
	public abstract class Specification : AutoMocker
	{
		private Type expectedExceptionType;
		private Exception thrownException;

		[TestFixtureSetUp]
		public void SetUp()
		{
			Given();

			try
			{
				When();
			}
			catch (Exception e)
			{
			    if (e.GetType() != this.expectedExceptionType)
					throw;
			    
                this.thrownException = e;
			}
		}

		protected void Expect<TException>() where TException : Exception
		{
			this.expectedExceptionType = typeof(TException);
		}

		protected TException Thrown<TException>() where TException : Exception
		{
			return this.thrownException as TException;
		}

		protected virtual void Given() {}
		protected abstract void When();

		[TestFixtureTearDown]
		public virtual void TidyUp()
		{
		}
	}
}