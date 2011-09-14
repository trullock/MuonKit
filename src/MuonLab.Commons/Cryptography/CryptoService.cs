using System;
using System.Security.Cryptography;
using System.Text;

namespace MuonLab.Commons.Cryptography
{
	public class CryptoService : ICryptoService
	{
		public string Hash(string plainText, Algorithm algorithm, byte[] salt)
		{
			if (string.IsNullOrEmpty(plainText))
				return plainText;

			// Convert plain text into a byte array.
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

			// Allocate array, which will hold plain text and salt.
			var plainTextWithSaltBytes =
				new byte[plainTextBytes.Length + salt.Length];

			// Copy plain text bytes into resulting array.
			for (var i = 0; i < plainTextBytes.Length; i++)
				plainTextWithSaltBytes[i] = plainTextBytes[i];

			// Append salt bytes to the resulting array.
			for (var i = 0; i < salt.Length; i++)
				plainTextWithSaltBytes[plainTextBytes.Length + i] = salt[i];

			// Because we support multiple hashing algorithms, we must define
			// hash object as a common (abstract) base class. We will specify the
			// actual hashing algorithm class later during object creation.
			HashAlgorithm hash;

			// Initialize appropriate hashing algorithm class.
			switch (algorithm)
			{
				case Algorithm.SHA1:
					hash = new SHA1Managed();
					break;

				case Algorithm.SHA256:
					hash = new SHA256Managed();
					break;

				case Algorithm.SHA384:
					hash = new SHA384Managed();
					break;

				case Algorithm.SHA512:
					hash = new SHA512Managed();
					break;

				default:
					hash = new MD5CryptoServiceProvider();
					break;
			}

			// Compute hash value of our plain text with appended salt.
			var hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

			// Create array which will hold hash and original salt bytes.
			var hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];

			// Copy hash bytes into resulting array.
			for (var i = 0; i < hashBytes.Length; i++)
				hashWithSaltBytes[i] = hashBytes[i];

			// Append salt bytes to the result.
			for (var i = 0; i < salt.Length; i++)
				hashWithSaltBytes[hashBytes.Length + i] = salt[i];

			// Convert result into a base64-encoded string.
			var hashValue = Convert.ToBase64String(hashWithSaltBytes);

			// Return the result.
			return hashValue;
		}

		public bool VerifyHash(string plainText, Algorithm hashAlgorithm, string hashValue)
		{
			// Convert base64-encoded hash value into a byte array.
			var hashWithSaltBytes = Convert.FromBase64String(hashValue);

			// We must know size of hash (without salt).
			int hashSizeInBits;


			// Size of hash is based on the specified algorithm.
			switch (hashAlgorithm)
			{
				case Algorithm.SHA1:
					hashSizeInBits = 160;
					break;

				case Algorithm.SHA256:
					hashSizeInBits = 256;
					break;

				case Algorithm.SHA384:
					hashSizeInBits = 384;
					break;

				case Algorithm.SHA512:
					hashSizeInBits = 512;
					break;

				default: // Must be MD5
					hashSizeInBits = 128;
					break;
			}

			// Convert size of hash from bits to bytes.
			var hashSizeInBytes = hashSizeInBits / 8;

			// Make sure that the specified hash value is long enough.
			if (hashWithSaltBytes.Length < hashSizeInBytes)
				return false;

			// Allocate array to hold original salt bytes retrieved from hash.
			var saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

			// Copy salt from the end of the hash to the new array.
			for (var i = 0; i < saltBytes.Length; i++)
				saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

			// Compute a new hash string.
			var expectedHashString = Hash(plainText, hashAlgorithm, saltBytes);

			// If the computed hash matches the specified hash,
			// the plain text value must be correct.
			return (hashValue == expectedHashString);
		}
	}
}