using System;
using _2c2pAssessment.Dal.Contracts;

namespace _2c2pAssessment.Dal
{
	public class FakeCardStorage : ICardStorage
	{
		public DateTime? GetCardExpiryDate(string cardNumber)
		{
			return new DateTime(2018, 1, 1);
		}
	}
}
