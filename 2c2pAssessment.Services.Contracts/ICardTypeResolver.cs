using _2c2pAssessment.Services.Contracts.Data;

namespace _2c2pAssessment.Services.Contracts
{
	public interface ICardTypeResolver
	{
		ICardTypeResult ResolveCardType(ICard card);
	}
}
