CREATE PROCEDURE sp_agregar_producto
(
	@Nombre NVARCHAR(100) = NULL,
	@Precio DECIMAL(10,2) = NULL,
	@Cantidad INT = NULL,
	@Estado BIT = NULL
)
AS
	INSERT INTO Producto(
		Nombre,
		Precio,
		Cantidad,
		Estado
	)
	VALUES
	(
		@Nombre,
		@Precio,
		@Cantidad,
		@Estado
	)
