using _2c2pAssessment.WebApi.Controllers;
using _2c2pAssessment.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _2c2pAssessment.Tests.WebApi
{
	[TestClass]
	public class CardsValidationControllerTests
	{
		[TestMethod]
		public void ModelIsNull()
		{
			var controller = new CardsValidationController();

			var result = controller.Post(null);

			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(422, (result as StatusCodeResult).StatusCode);
		}

		[TestMethod]
		public void CardNumberIsNull()
		{
			var controller = new CardsValidationController();

			var model = new SubmittedCardModel() { CardNumber = null };

			var result = controller.Post(model);

			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(422, (result as StatusCodeResult).StatusCode);
		}

		[TestMethod]
		public void CardNumberIsEmpty()
		{
			var controller = new CardsValidationController();

			var model = new SubmittedCardModel() { CardNumber = "" };

			var result = controller.Post(model);

			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(422, (result as StatusCodeResult).StatusCode);
		}
	}
}
