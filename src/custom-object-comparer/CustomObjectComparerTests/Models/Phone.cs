using System;

namespace CustomObjectComparerTests
{
	public struct Phone
	{
		public string CountryCode { get; set; }
		
		public string Number { get; set; }

		public Phone(string phone)
		{
			var separator = phone.IndexOf('|');
			if (separator < 0) throw new FormatException("Invalid phone format.");
			CountryCode = phone.Remove(separator);
			Number = phone.Substring(separator + 1);
		}

		public override string ToString()
		{
			return $"{CountryCode}|{Number}";
		}
	}
}
