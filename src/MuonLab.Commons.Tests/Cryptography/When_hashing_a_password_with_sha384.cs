using MuonLab.Commons.Cryptography;

namespace MuonLab.Commons.Tests.Cryptography
{
	public class When_hashing_a_password_with_sha384 : CryptoServiceFixture
	{
		protected override void Given()
		{
			this.algorithm = Algorithm.SHA384;
		}
	}
}