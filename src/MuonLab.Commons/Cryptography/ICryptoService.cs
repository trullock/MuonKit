namespace MuonLab.Commons.Cryptography
{
	public interface ICryptoService
	{
        /// <summary>
        /// Hashes a plaintext with the given algorithm and salt
        /// </summary>
        /// <param name="plainText">The text to hash</param>
        /// <param name="algorithm">The algorithm to use</param>
        /// <param name="salt">The cryptographic salt</param>
        /// <returns>Ciphertext</returns>
		string Hash(string plainText, Algorithm algorithm, byte[] salt);

		bool VerifyHash(string plainText, Algorithm hashAlgorithm, string hashValue);
	}
}