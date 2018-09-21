CREATE PROCEDURE [cards].[FindCardByCardNumber]
	@cardNumber char(16)
AS
	SELECT 
		c.ExpiryDate as ExpiryDate
	FROM
		[cards].[Card] c
	WHERE
		c.CardNumber = @cardNumber