using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2c2pAssessment.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace _2c2pAssessment.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CardsValidationController : BaseController
	{
		public CardsValidationController()
		{
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

			return new JsonResult(new ValidationResultModel(), jsonSerializerSettings);
		}
	}
}