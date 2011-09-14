using System.Text;
using MuonLab.Commons.Cryptography;
using MuonLab.Testing;
using NUnit.Framework;

namespace MuonLab.Commons.Tests.Cryptography
{
	public abstract class CryptoServiceFixture : Specification
	{
		protected Algorithm algorithm;
		private CryptoService subject;

		protected override void When()
		{
			this.subject = new CryptoService();
		}

		[Test]
		public void different_salts_should_produce_different_passwords()
		{
			var hash = this.subject.Hash("a password", this.algorithm, Encoding.ASCII.GetBytes("a salt"));
			var hash2 = this.subject.Hash("a password", this.algorithm, Encoding.ASCII.GetBytes("a different salt"));

			Assert.AreNotEqual(hash, hash2);
		}

		[Test]
		public void hashes_should_be_repeatable()
		{
			var hash = this.subject.Hash("a password", this.algorithm, Encoding.ASCII.GetBytes("a salt"));
			var hash2 = this.subject.Hash("a password", this.algorithm, Encoding.ASCII.GetBytes("a salt"));

			Assert.AreEqual(hash, hash2);
		}

		[Test]
		public void hashes_should_pass_verification()
		{
			var hash = this.subject.Hash("a password", this.algorithm, Encoding.ASCII.GetBytes("a salt"));
			Assert.IsTrue(this.subject.VerifyHash("a password", this.algorithm, hash));
		}

		[Test]
		public void different_hashes_should_fail_verification()
		{
			var hash = this.subject.Hash("a password", this.algorithm, Encoding.ASCII.GetBytes("a salt"));
			Assert.IsFalse(this.subject.VerifyHash("a different password", this.algorithm, hash));
		}

		[Test]
		public void an_empty_password_should_produce_an_empty_hash()
		{
			var hash = this.subject.Hash(string.Empty, this.algorithm, Encoding.ASCII.GetBytes("a salt"));
			Assert.IsTrue(hash == string.Empty);
		}

		[Test]
		public void a_null_password_should_produce_a_null_hash()
		{
			var hash = this.subject.Hash(null, this.algorithm, Encoding.ASCII.GetBytes("a salt"));
			Assert.IsNull(hash);
		}
	}
}