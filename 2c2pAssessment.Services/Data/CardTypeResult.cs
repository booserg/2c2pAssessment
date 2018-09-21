using _2c2pAssessment.Services.Contracts.Data;

namespace _2c2pAssessment.Services.Data
{
	public class CardTypeResult : ICardTypeResult
	{
		public CardTypeResult(CardType cardType)
		{
			CardType = cardType;
		}

		public CardType CardType { get; private set; }
	}
}
