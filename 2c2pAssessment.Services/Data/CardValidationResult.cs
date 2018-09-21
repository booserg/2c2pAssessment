using _2c2pAssessment.Services.Contracts.Data;

namespace _2c2pAssessment.Services.Data
{
	public class CardValidationResult : ICardValidationResult
	{
		public CardValidationResult(ValidationState result, CardType type)
		{
			ValidationResult = result;
			CardType = type;
		}

		public ValidationState ValidationResult { get; private set; }

		public CardType CardType { get; private set; }
	}
}
