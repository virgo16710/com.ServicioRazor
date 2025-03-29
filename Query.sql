use[MiBD];
GO
IF OBJECT_ID('GETREGIONES', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE DBO.GETREGIONES;
	END
GO
CREATE PROCEDURE GETREGIONES
AS
BEGIN
SELECT * FROM DBO.Region
END
GO
IF OBJECT_ID('GETREGION', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE DBO.GETREGION;
	END
GO
CREATE PROCEDURE GETREGION
 @id int
AS
BEGIN
SELECT * FROM DBO.Region where IdRegion = @id
END
GO
IF OBJECT_ID('GETCOMUNAS', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE DBO.GETCOMUNAS;
	END
GO
CREATE PROCEDURE GETCOMUNAS
 @id int
AS
BEGIN
SELECT * FROM DBO.Comuna where IdRegion = @id
END
GO
IF OBJECT_ID('GETCOMUNA', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE DBO.GETCOMUNA;
	END
GO
CREATE PROCEDURE GETCOMUNA
 @idRegion int,
 @IdComuna int
AS
BEGIN
SELECT * FROM DBO.Comuna where IdRegion = @idRegion and IdComuna = @IdComuna
END
GO
IF OBJECT_ID('MERGCOMUNA', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE DBO.MERGCOMUNA;
	END
GO
CREATE PROCEDURE MERGCOMUNA
 @idRegion int,
 @IdComuna int,
 @Comuna varchar(128),
 @info text
AS
BEGIN
	MERGE INTO dbo.Comuna as destino
	using(select @idRegion as idRegion,@IdComuna as IdComuna,@Comuna as Comuna,@info as Info) as origen
	on destino.IdComuna = origen.IdComuna
	when matched then
		update set
		destino.IdRegion = origen.idRegion,
		destino.Comuna = origen.Comuna,
		destino.InformacionAdicional = origen.info
	when not matched then
	insert (IdRegion,Comuna,InformacionAdicional)
	values(origen.idRegion,origen.Comuna,origen.info);
END
GO
