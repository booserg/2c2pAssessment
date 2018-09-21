using _2c2pAssessment.Services;
using _2c2pAssessment.Services.Contracts.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace _2c2pAssessment.Tests.Services
{
	[TestClass]
	public class CardTypeResolverTests
	{
		[TestMethod]
		public void IfCardNumberIsLongerThen16Chars()
		{
			var cardResolver = new CardTypeResolver();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns("11111111111111111");

			var result = cardResolver.ResolveCardType(card.Object);

			Assert.AreEqual(CardType.Unknown, result.CardType);
		}

		[TestMethod]
		public void IfCardNumberIsShorterThen15Chars()
		{
			var cardResolver = new CardTypeResolver();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns("11111111111111");

			var result = cardResolver.ResolveCardType(card.Object);

			Assert.AreEqual(CardType.Unknown, result.CardType);
		}

		[TestMethod]
		public void IfCard16CharsLongAndStartsWith4()
		{
			var cardResolver = new CardTypeResolver();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns("4444444444444444");

			var result = cardResolver.ResolveCardType(card.Object);

			Assert.AreEqual(CardType.Visa, result.CardType);
		}

		[TestMethod]
		public void IfCard16CharsLongAndStartsWith5()
		{
			var cardResolver = new CardTypeResolver();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns("5555555555555555");

			var result = cardResolver.ResolveCardType(card.Object);

			Assert.AreEqual(CardType.Master, result.CardType);
		}

		[TestMethod]
		public void IfCard16CharsLongAndStartsWith3()
		{
			var cardResolver = new CardTypeResolver();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns("3333333333333333");

			var result = cardResolver.ResolveCardType(card.Object);

			Assert.AreEqual(CardType.JCB, result.CardType);
		}

		[TestMethod]
		public void IfCard15CharsLongAndStartsWith3()
		{
			var cardResolver = new CardTypeResolver();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns("333333333333333");

			var result = cardResolver.ResolveCardType(card.Object);

			Assert.AreEqual(CardType.Amex, result.CardType);
		}

		[TestMethod]
		public void IfCard16CharsLongAndStartsWith1()
		{
			var cardResolver = new CardTypeResolver();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns("1111111111111111");

			var result = cardResolver.ResolveCardType(card.Object);

			Assert.AreEqual(CardType.Unknown, result.CardType);
		}

		[TestMethod]
		public void IfCard15CharsLongAndStartsWith4()
		{
			var cardResolver = new CardTypeResolver();

			var card = new Mock<ICard>();
			card.SetupGet(c => c.CardNumber).Returns("444444444444444");

			var result = cardResolver.ResolveCardType(card.Object);

			Assert.AreEqual(CardType.Unknown, result.CardType);
		}
	}
}
