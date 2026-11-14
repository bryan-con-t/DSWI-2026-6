CREATE PROCEDURE sp_buscar_productos_paginados
(
	@pageSize INT = 5, /* Cantidad de elementos por página */
    @page INT = 1, /* Índice de la página */
	@nombre NVARCHAR(100) = ''
)
AS
BEGIN
	SET NOCOUNT ON /* Desactiva el conteo de filas AFECTADAS */
	DECLARE @inicio INT = (@page - 1) * @pageSize /* Variable inicio de página */
    SELECT * FROM Producto
	WHERE Nombre LIKE '%' + @nombre + '%'
	ORDER BY Nombre
	OFFSET @inicio ROWS /* Inicio de la paginación */
	FETCH NEXT @pageSize ROWS ONLY /* Elementos paginados */
END
