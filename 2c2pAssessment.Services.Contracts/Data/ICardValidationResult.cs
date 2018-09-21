namespace _2c2pAssessment.Services.Contracts.Data
{
	public interface ICardValidationResult
	{
		ValidationState ValidationResult { get; }

		CardType CardType { get; }
	}
}
