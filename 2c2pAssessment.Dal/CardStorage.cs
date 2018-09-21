using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using dc = _2c2pAssessment.DataContext;
using _2c2pAssessment.Dal.Contracts;

namespace _2c2pAssessment.Dal
{
	public class CardStorage : ICardStorage
	{
		private Func<dc.DataContext> _dataContextCreator;

		public CardStorage(Func<dc.DataContext> dataContextCreator)
		{
			_dataContextCreator = dataContextCreator;
		}

		public DateTime? GetCardExpiryDate(string cardNumber)
		{
			using (var ctx = _dataContextCreator())
			using (var command = ctx.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = "[cards].[FindCardByCardNumber]";
				command.CommandType = CommandType.StoredProcedure;
				SqlParameter param = new SqlParameter() { ParameterName = "@cardNumber", Value = cardNumber };
				command.Parameters.Add(param);

				ctx.Database.OpenConnection();

				var dataReader = command.ExecuteReader();

				if (dataReader.Read())
				{
					var res = dataReader.GetDateTime(dataReader.GetOrdinal("ExpiryDate"));
					return res;
				}
				return null;
			}
		}
	}
}
