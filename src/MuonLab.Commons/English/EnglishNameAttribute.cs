using System;

namespace MuonLab.Commons.English
{
	/// <summary>
	/// Attribute used to describe in english a property's meaning that would otherwise be non-obvious
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class EnglishNameAttribute : Attribute
	{
		/// <summary>
		/// The name of the property
		/// </summary>
		public string Name { get; set; }
	}
}