using _2c2pAssessment.Services.Contracts.Data;

namespace _2c2pAssessment.Services.Data
{
	public class Card : ICard
	{
		public Card(string cardNumber)
		{
			CardNumber = cardNumber;
		}

		public string CardNumber { get; private set; }
	}
}
