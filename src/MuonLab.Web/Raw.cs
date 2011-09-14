namespace MuonLab.Web
{
	public class Raw : IRawHtml
	{
		public string Value { get; set; }

		public static explicit operator Raw(string text)
		{
			return new Raw { Value = text };
		}

		public static implicit operator string(Raw output)
		{
			if(output == null)
				return null;

			return output.Value;
		}

		public override string ToString()
		{
			return this.Value;
		}
	}
}