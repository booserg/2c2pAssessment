using _2c2pAssessment.Services.Contracts;
using _2c2pAssessment.Services.Contracts.Data;
using _2c2pAssessment.Services.Data;
using _2c2pAssessment.WebApi.Controllers;
using _2c2pAssessment.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace _2c2pAssessment.Tests.WebApi
{
	[TestClass]
	public class CardsValidationControllerTests
	{
		[TestMethod]
		public void ModelIsNull()
		{
			var cardValidator = new Mock<ICardValidator>();
			var numberService = new Mock<INumberService>();

			var controller = new CardsValidationController(cardValidator.Object, numberService.Object);

			var result = controller.Post(null);

			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(422, (result as StatusCodeResult).StatusCode);
		}

		[TestMethod]
		public void CardNumberIsNull()
		{
			var cardValidator = new Mock<ICardValidator>();
			var numberService = new Mock<INumberService>();

			var controller = new CardsValidationController(cardValidator.Object, numberService.Object);

			var model = new SubmittedCardModel() { CardNumber = null };

			var result = controller.Post(model);

			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(422, (result as StatusCodeResult).StatusCode);
		}

		[TestMethod]
		public void CardNumberIsEmpty()
		{
			var cardValidator = new Mock<ICardValidator>();
			var numberService = new Mock<INumberService>();

			var controller = new CardsValidationController(cardValidator.Object, numberService.Object);

			var model = new SubmittedCardModel() { CardNumber = "" };

			var result = controller.Post(model);

			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(422, (result as StatusCodeResult).StatusCode);
		}

		[TestMethod]
		public void CardNumberWithDigitsOnly()
		{
			var cardValidationResult = new Mock<ICardValidationResult>();

			var cardValidator = new Mock<ICardValidator>();
			cardValidator.Setup(cv => cv.ValidateCard(It.IsAny<Card>())).Returns(cardValidationResult.Object);

			var numberService = new Mock<INumberService>();
			numberService.Setup(s => s.IsDigitsOnly(It.IsAny<string>())).Returns(true);

			var controller = new CardsValidationController(cardValidator.Object, numberService.Object);

			var model = new SubmittedCardModel() { CardNumber = "123" };

			var result = controller.Post(model);

			numberService.Verify(mock => mock.IsDigitsOnly(It.IsAny<string>()), Times.Once());
		}

		[TestMethod]
		public void CardNumberWithChar()
		{
			var cardValidator = new Mock<ICardValidator>();
			cardValidator.Setup(cv => cv.ValidateCard(It.IsAny<Card>()));

			var numberService = new Mock<INumberService>();
			numberService.Setup(s => s.IsDigitsOnly(It.IsAny<string>())).Returns(false);

			var controller = new CardsValidationController(cardValidator.Object, numberService.Object);

			var model = new SubmittedCardModel() { CardNumber = "asd" };

			var result = controller.Post(model);

			numberService.Verify(mock => mock.IsDigitsOnly(It.IsAny<string>()), Times.Once());

			Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
			Assert.AreEqual(422, (result as StatusCodeResult).StatusCode);
		}
	}
}
