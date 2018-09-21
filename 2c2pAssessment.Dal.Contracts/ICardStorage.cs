using System;

namespace _2c2pAssessment.Dal.Contracts
{
	public interface ICardStorage
	{
		DateTime? GetCardExpiryDate(string cardNumber);
	}
}
