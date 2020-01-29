use Game;

go
if object_id ('dbo.AddItem', 'P') is not null
	drop procedure dbo.AddItem

go
create procedure dbo.AddItem
	@name nvarchar(30),
	@cost int,
	@type nvarchar(30),
	@data nvarchar(1000)
as
	insert into dbo.Items(Name, Cost, Type, Data)
		output inserted.Id
		values (@name, @cost, @type, @data)