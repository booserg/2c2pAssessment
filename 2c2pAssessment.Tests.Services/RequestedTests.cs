using _2c2pAssessment.Dal.Contracts;
using _2c2pAssessment.Services;
using _2c2pAssessment.Services.Contracts;
using _2c2pAssessment.Services.Contracts.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace _2c2pAssessment.Tests.Services
{
	[TestClass]
	public class RequestedTests
	{
		[TestMethod]
		public void ValidVisa()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2024, 01, 01));

			var cardTypeResult = new Mock<ICardTypeResult>();
			cardTypeResult.Setup(ctr => ctr.CardType).Returns(CardType.Visa);

			var cardTypeResolver = new Mock<ICardTypeResolver>();
			cardTypeResolver.Setup(s => s.ResolveCardType(It.IsAny<ICard>())).Returns(cardTypeResult.Object);

			var numberService = new Mock<INumberService>();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns(It.IsAny<String>());

			var cardValidator = new CardValidator(storage.Object, cardTypeResolver.Object, numberService.Object);
			var res = cardValidator.ValidateCard(card.Object);

			Assert.AreEqual(ValidationState.Valid, res.ValidationResult);
		}

		[TestMethod]
		public void InvalidVisa()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2023, 01, 01));

			var cardTypeResult = new Mock<ICardTypeResult>();
			cardTypeResult.Setup(ctr => ctr.CardType).Returns(CardType.Visa);

			var cardTypeResolver = new Mock<ICardTypeResolver>();
			cardTypeResolver.Setup(s => s.ResolveCardType(It.IsAny<ICard>())).Returns(cardTypeResult.Object);

			var numberService = new Mock<INumberService>();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns(It.IsAny<String>());

			var cardValidator = new CardValidator(storage.Object, cardTypeResolver.Object, numberService.Object);
			var res = cardValidator.ValidateCard(card.Object);

			Assert.AreEqual(ValidationState.Invalid, res.ValidationResult);
		}

		[TestMethod]
		public void ValidMasterCard()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2024, 01, 01));

			var cardTypeResult = new Mock<ICardTypeResult>();
			cardTypeResult.Setup(ctr => ctr.CardType).Returns(CardType.Master);

			var cardTypeResolver = new Mock<ICardTypeResolver>();
			cardTypeResolver.Setup(s => s.ResolveCardType(It.IsAny<ICard>())).Returns(cardTypeResult.Object);

			var numberService = new Mock<INumberService>();
			numberService.Setup(ns => ns.IsPrime(It.IsAny<int>())).Returns(true);

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns(It.IsAny<String>());

			var cardValidator = new CardValidator(storage.Object, cardTypeResolver.Object, numberService.Object);
			var res = cardValidator.ValidateCard(card.Object);

			Assert.AreEqual(ValidationState.Valid, res.ValidationResult);
		}

		[TestMethod]
		public void InvalidMasterCard()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2024, 01, 01));

			var cardTypeResult = new Mock<ICardTypeResult>();
			cardTypeResult.Setup(ctr => ctr.CardType).Returns(CardType.Master);

			var cardTypeResolver = new Mock<ICardTypeResolver>();
			cardTypeResolver.Setup(s => s.ResolveCardType(It.IsAny<ICard>())).Returns(cardTypeResult.Object);

			var numberService = new Mock<INumberService>();
			numberService.Setup(ns => ns.IsPrime(It.IsAny<int>())).Returns(false);

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns(It.IsAny<String>());

			var cardValidator = new CardValidator(storage.Object, cardTypeResolver.Object, numberService.Object);
			var res = cardValidator.ValidateCard(card.Object);

			Assert.AreEqual(ValidationState.Invalid, res.ValidationResult);
		}

		[TestMethod]
		public void ValidJCB()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2024, 01, 01));

			var cardTypeResult = new Mock<ICardTypeResult>();
			cardTypeResult.Setup(ctr => ctr.CardType).Returns(CardType.JCB);

			var cardTypeResolver = new Mock<ICardTypeResolver>();
			cardTypeResolver.Setup(s => s.ResolveCardType(It.IsAny<ICard>())).Returns(cardTypeResult.Object);

			var numberService = new Mock<INumberService>();
			numberService.Setup(ns => ns.IsPrime(It.IsAny<int>())).Returns(false);

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns(It.IsAny<String>());

			var cardValidator = new CardValidator(storage.Object, cardTypeResolver.Object, numberService.Object);
			var res = cardValidator.ValidateCard(card.Object);

			Assert.AreEqual(ValidationState.Valid, res.ValidationResult);
		}

		[TestMethod]
		public void InvalidAmex()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2024, 01, 01));

			var cardTypeResult = new Mock<ICardTypeResult>();
			cardTypeResult.Setup(ctr => ctr.CardType).Returns(CardType.Amex);

			var cardTypeResolver = new Mock<ICardTypeResolver>();
			cardTypeResolver.Setup(s => s.ResolveCardType(It.IsAny<ICard>())).Returns(cardTypeResult.Object);

			var numberService = new Mock<INumberService>();
			numberService.Setup(ns => ns.IsPrime(It.IsAny<int>())).Returns(false);

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns(It.IsAny<String>());

			var cardValidator = new CardValidator(storage.Object, cardTypeResolver.Object, numberService.Object);
			var res = cardValidator.ValidateCard(card.Object);

			Assert.AreEqual(ValidationState.Invalid, res.ValidationResult);
		}
	}
}
