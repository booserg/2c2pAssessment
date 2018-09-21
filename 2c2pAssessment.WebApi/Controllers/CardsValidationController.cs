using System;
using _2c2pAssessment.Services.Contracts;
using _2c2pAssessment.Services.Data;
using _2c2pAssessment.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace _2c2pAssessment.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CardsValidationController : BaseController
	{
		private readonly ICardValidator _cardValidator;
		private readonly INumberService _numberService;

		public CardsValidationController(ICardValidator cardValidator, INumberService numberService)
		{
			_cardValidator = cardValidator;
			_numberService = numberService;
		}

		/// <summary>
		/// Validate a card number.
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///	POST /api/CardsValidation
		///	{
		///		"CardNumber":"4444444444444444"
		///	}
		///
		/// </remarks>
		/// <param name="card"></param>
		/// <returns>Validation result</returns>
		/// <response code="422">If the card number consist of nondigits charecter OR If the card number is null OR If the card number is empty</response>
		[ProducesResponseType(typeof(ValidationResultModel), 200)]
		[ProducesResponseType(422)]
		[HttpPost]
		public IActionResult Post([FromBody] SubmittedCardModel card)
		{
			if (card == null || card.CardNumber == null || String.IsNullOrEmpty(card.CardNumber))
				return StatusCode(422);

			if (!_numberService.IsDigitsOnly(card.CardNumber))
				return StatusCode(422);

			var validationResult = _cardValidator.ValidateCard(new Card(card.CardNumber));

			return new JsonResult(new ValidationResultModel(validationResult.CardType, validationResult.ValidationResult), jsonSerializerSettings);
		}
	}
}