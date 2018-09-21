CREATE TABLE [cards].[Card] (
    [Id]         INT       IDENTITY (1, 1) NOT NULL,
    [CardNumber] CHAR (16) NOT NULL,
    [ExpiryDate] DATE      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [CK_Card_CardNumber] UNIQUE ([CardNumber])
);

GO

CREATE INDEX [IX_Card_CardNumber] ON [cards].[Card] ([CardNumber])
