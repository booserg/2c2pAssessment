using _2c2pAssessment.Dal.Contracts;
using _2c2pAssessment.Services.Contracts;
using _2c2pAssessment.Services.Contracts.Data;
using _2c2pAssessment.Services.Data;
using System;

namespace _2c2pAssessment.Services
{
	public class CardValidator : ICardValidator
	{
		ICardStorage _storage;
		ICardTypeResolver _cardTypeResolver;
		INumberService _numberService;

		public CardValidator(ICardStorage storage, ICardTypeResolver cardTypeResolver, INumberService numberService)
		{
			_storage = storage;
			_cardTypeResolver = cardTypeResolver;
			_numberService = numberService;
		}

		public ICardValidationResult ValidateCard(ICard card)
		{
			var cardType = _cardTypeResolver.ResolveCardType(card);
			if (cardType.CardType == CardType.Unknown)
				return new CardValidationResult(ValidationState.Invalid, CardType.Unknown);

			var expiryDate = _storage.GetCardExpiryDate(card.CardNumber);

			if (!expiryDate.HasValue)//card is't exist in DB
				return new CardValidationResult(ValidationState.DoesNotExist, cardType.CardType);
			else if (cardType.CardType == CardType.Visa && DateTime.IsLeapYear(expiryDate.Value.Year))
				return new CardValidationResult(ValidationState.Valid, cardType.CardType);
			else if (cardType.CardType == CardType.Master && _numberService.IsPrime(expiryDate.Value.Year))
				return new CardValidationResult(ValidationState.Valid, cardType.CardType);
			else if (cardType.CardType == CardType.JCB)
				return new CardValidationResult(ValidationState.Valid, cardType.CardType);
			else
				return new CardValidationResult(ValidationState.Invalid, cardType.CardType);
		}
	}
}
