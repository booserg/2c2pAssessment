using _2c2pAssessment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _2c2pAssessment.Tests.Services
{
	[TestClass]
	public class NumberServiceTests
	{
		[TestMethod]
		public void IsPrimeNumber0()
		{
			var numberService = new NumberService();

			var res = numberService.IsPrime(0);

			Assert.IsFalse(res);
		}

		[TestMethod]
		public void IsPrimeNumber1()
		{
			var numberService = new NumberService();

			var res = numberService.IsPrime(1);

			Assert.IsFalse(res);
		}

		[TestMethod]
		public void IsPrimeNumber2()
		{
			var numberService = new NumberService();

			var res = numberService.IsPrime(2);

			Assert.IsTrue(res);
		}

		[TestMethod]
		public void IsPrimeNumber3()
		{
			var numberService = new NumberService();

			var res = numberService.IsPrime(3);

			Assert.IsTrue(res);
		}

		[TestMethod]
		public void IsPrimeNumber4()
		{
			var numberService = new NumberService();

			var res = numberService.IsPrime(4);

			Assert.IsFalse(res);
		}

		[TestMethod]
		public void IsPrimeNumber29()
		{
			var numberService = new NumberService();

			var res = numberService.IsPrime(29);

			Assert.IsTrue(res);
		}

		[TestMethod]
		public void IsNullDigigtsOnly()
		{
			var numberService = new NumberService();

			var res = numberService.IsDigitsOnly(null);

			Assert.IsFalse(res);
		}

		[TestMethod]
		public void IsZeroLengthStringDigigtsOnly()
		{
			var numberService = new NumberService();

			var res = numberService.IsDigitsOnly("");

			Assert.IsFalse(res);
		}

		[TestMethod]
		public void IsShortDigitalStringDigigtsOnly()
		{
			var numberService = new NumberService();

			var res = numberService.IsDigitsOnly("12312");

			Assert.IsTrue(res);
		}

		[TestMethod]
		public void IsLongDigitalStringDigigtsOnly()
		{
			var numberService = new NumberService();

			var res = numberService.IsDigitsOnly("12342342342343242342334234324234312");

			Assert.IsTrue(res);
		}

		[TestMethod]
		public void IsShortNonDigitalStringDigigtsOnly()
		{
			var numberService = new NumberService();

			var res = numberService.IsDigitsOnly("123a123");

			Assert.IsFalse(res);
		}

		[TestMethod]
		public void IsLongNonDigitalStringDigigtsOnly()
		{
			var numberService = new NumberService();

			var res = numberService.IsDigitsOnly("123324324234234234234234123basdasdas12312312312312312312");

			Assert.IsFalse(res);
		}
	}
}
