using _2c2pAssessment.Services.Contracts.Data;

namespace _2c2pAssessment.Services.Contracts
{
	public interface ICardValidator
	{
		ICardValidationResult ValidateCard(ICard card);
	}
}
