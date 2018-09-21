using _2c2pAssessment.Services.Contracts;
using _2c2pAssessment.Services.Contracts.Data;
using _2c2pAssessment.Services.Data;

namespace _2c2pAssessment.Services
{
	public class CardTypeResolver : ICardTypeResolver
	{
		public ICardTypeResult ResolveCardType(ICard card)
		{
			if (card.CardNumber.Length > 16 || card.CardNumber.Length < 15)
				return new CardTypeResult(CardType.Unknown);

			if (card.CardNumber.StartsWith('4') && card.CardNumber.Length == 16)
				return new CardTypeResult(CardType.Visa);

			if (card.CardNumber.StartsWith('5') && card.CardNumber.Length == 16)
				return new CardTypeResult(CardType.Master);

			if (card.CardNumber.StartsWith('3') && card.CardNumber.Length == 16)
				return new CardTypeResult(CardType.JCB);

			if (card.CardNumber.StartsWith('3') && card.CardNumber.Length == 15)
				return new CardTypeResult(CardType.Amex);

			return new CardTypeResult(CardType.Unknown);
		}
	}
}
