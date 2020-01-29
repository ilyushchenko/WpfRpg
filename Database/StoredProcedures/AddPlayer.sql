use Game;

go
if object_id ('dbo.AddPlayer', 'P') is not null
	drop procedure dbo.AddPlayer

go
create procedure dbo.AddPlayer
	@login nvarchar(100)
as

BEGIN TRANSACTION
	declare @IdTable table (ID uniqueidentifier)
	
	INSERT INTO dbo.Players(Login)
	output inserted.ID into @IdTable
	values (@login)

    IF (@@error <> 0)
        ROLLBACK
    
	declare @Id as uniqueidentifier
	
	SET @Id = (SELECT ID FROM @IdTable)

	INSERT INTO dbo.PlayerStatistics(PlayerId, Winrate) values (@Id, 0)
    
    IF (@@error <> 0)
        ROLLBACK
COMMIT