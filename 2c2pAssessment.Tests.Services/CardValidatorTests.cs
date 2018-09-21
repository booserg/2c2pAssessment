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
	public class CardValidatorTests
	{
		[TestMethod]
		public void UnknownCardShouldBeInvalid()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2018, 01, 01));

			var cardTypeResult = new Mock<ICardTypeResult>();
			cardTypeResult.Setup(ctr => ctr.CardType).Returns(CardType.Unknown);

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
		public void MissedInDBVisaCardShouldBeDoesNotExist()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns((Nullable<DateTime>)null);

			var cardTypeResult = new Mock<ICardTypeResult>();
			cardTypeResult.Setup(ctr => ctr.CardType).Returns(CardType.Visa);

			var cardTypeResolver = new Mock<ICardTypeResolver>();
			cardTypeResolver.Setup(s => s.ResolveCardType(It.IsAny<ICard>())).Returns(cardTypeResult.Object);

			var numberService = new Mock<INumberService>();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns(It.IsAny<String>());

			var cardValidator = new CardValidator(storage.Object, cardTypeResolver.Object, numberService.Object);
			var res = cardValidator.ValidateCard(card.Object);

			Assert.AreEqual(ValidationState.DoesNotExist, res.ValidationResult);
		}

		[TestMethod]
		public void MissedInDBMasterCardShouldBeDoesNotExist()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns((Nullable<DateTime>)null);

			var cardTypeResult = new Mock<ICardTypeResult>();
			cardTypeResult.Setup(ctr => ctr.CardType).Returns(CardType.Master);

			var cardTypeResolver = new Mock<ICardTypeResolver>();
			cardTypeResolver.Setup(s => s.ResolveCardType(It.IsAny<ICard>())).Returns(cardTypeResult.Object);

			var numberService = new Mock<INumberService>();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns(It.IsAny<String>());

			var cardValidator = new CardValidator(storage.Object, cardTypeResolver.Object, numberService.Object);
			var res = cardValidator.ValidateCard(card.Object);

			Assert.AreEqual(ValidationState.DoesNotExist, res.ValidationResult);
		}

		[TestMethod]
		public void VisaCardShouldBeValidIfLeapYearOfExpiryDate()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2024, 1, 1));

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
		public void VisaCardShouldBeInvalidIfExpiryDateYearIsNotLeap()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2023, 1, 1));

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
		public void MasterCardShouldBeInvalidIfExpiryDateYearIsNotPrimeNumber()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2023, 1, 1));

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
		public void MasterCardShouldValidIfExpiryDateYearIsPrimeNumber()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2023, 1, 1));

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
		public void JCBCardShouldAlwaysBeValid()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2023, 1, 1));

			var cardTypeResult = new Mock<ICardTypeResult>();
			cardTypeResult.Setup(ctr => ctr.CardType).Returns(CardType.JCB);

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
		public void AmexCardShouldAlwaysBeInvalid()
		{
			var storage = new Mock<ICardStorage>();
			storage.Setup(s => s.GetCardExpiryDate(It.IsAny<String>())).Returns(new DateTime(2023, 1, 1));

			var cardTypeResult = new Mock<ICardTypeResult>();
			cardTypeResult.Setup(ctr => ctr.CardType).Returns(CardType.Amex);

			var cardTypeResolver = new Mock<ICardTypeResolver>();
			cardTypeResolver.Setup(s => s.ResolveCardType(It.IsAny<ICard>())).Returns(cardTypeResult.Object);

			var numberService = new Mock<INumberService>();
			numberService.Setup(ns => ns.IsPrime(It.IsAny<int>())).Returns(true);

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns(It.IsAny<String>());

			var cardValidator = new CardValidator(storage.Object, cardTypeResolver.Object, numberService.Object);
			var res = cardValidator.ValidateCard(card.Object);

			Assert.AreEqual(ValidationState.Invalid, res.ValidationResult);
		}
	}
}
