using System.Collections.Generic;

namespace CustomObjectComparerTests
{
	public class Client
	{
		public int ID { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public double Rank { get; set; }

		public Phone PrimaryPhone { get; set; }

		public ICollection<Phone> AlternativePhones { get; set; }

		public Title PrimaryTitle { get; set; }
		
		public ICollection<Title> AlternativeTitles { get; set; }

		public Address PrimaryAddress { get; set; }

		public ICollection<Address> AlternativeAddresses { get; set; }

		private readonly int[] PrivateArray;

		private readonly Dictionary<int, int> PrivateDict;

		public Client(int[] privateArray, Dictionary<int, int> privateDict)
		{
			PrivateArray = privateArray;
			PrivateDict = privateDict;
		}

		public int[] GetPrivateArray()
		{
			return PrivateArray;
		}

		public Dictionary<int, int> GetPrivateDict()
		{
			return PrivateDict;
		}
	}
}
