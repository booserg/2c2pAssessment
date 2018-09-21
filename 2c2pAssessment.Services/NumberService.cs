using _2c2pAssessment.Services.Contracts;
using System;

namespace _2c2pAssessment.Services
{
	public class NumberService : INumberService
	{
		public bool IsPrime(int number)
		{
			if (number <= 1) return false;
			if (number == 2) return true;
			if (number % 2 == 0) return false;

			var boundary = (int)Math.Floor(Math.Sqrt(number));

			for (int i = 3; i <= boundary; i += 2)
			{
				if (number % i == 0) return false;
			}

			return true;
		}

		public bool IsDigitsOnly(string str)
		{
			if (String.IsNullOrEmpty(str))
				return false;

			foreach (char c in str)
			{
				if (c < '0' || c > '9')
					return false;
			}

			return true;
		}
	}
}
