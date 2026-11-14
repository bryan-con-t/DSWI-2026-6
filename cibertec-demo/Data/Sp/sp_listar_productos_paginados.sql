CREATE PROCEDURE sp_listar_productos_paginados
(
	@pageSize INT = 5, /* Cantidad de elementos por página */
	@page INT = 1 /* Índice de la página */
)
AS
BEGIN
	SET NOCOUNT ON /* Desactiva el conteo de filas AFECTADAS */
	DECLARE @inicio INT = (@page - 1) * @pageSize /* Variable inicio de página */
	SELECT * FROM Producto
	ORDER BY Nombre
	OFFSET @inicio ROWS /* Inicio de la paginación */
	FETCH NEXT @pageSize ROWS ONLY /* Elementos paginados */
END
