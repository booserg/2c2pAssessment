using _2c2pAssessment.Services.Contracts.Data;

namespace _2c2pAssessment.WebApi.Models
{
	public class ValidationResultModel
	{
		public ValidationResultModel(CardType cardType, ValidationState validationState)
		{
			CardType = cardType;
			ValidationResult = validationState;
		}

		public CardType CardType { get; private set; }

		public ValidationState ValidationResult { get; private set; }
	}
}
