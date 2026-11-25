using cibertec_demo.Models;
using Microsoft.Data.SqlClient;

namespace cibertec_demo.Data
{
    public class ProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(IConfiguration configuracion)
        {
            _connectionString = configuracion.GetConnectionString("DefaultConnection");
        }

        // Función para insertar usando consulta SQL
        public async Task AgregarProductoSql(Producto producto)
        {
            var sql = "INSERT INTO Producto(Nombre, Precio, Cantidad, Estado) VALUES (@Nombre, @Precio, @Cantidad, @Estado)";
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                cmd.Parameters.AddWithValue("@Estado", producto.Estado);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        // Función para insertar usando procedimiento almacenado
        public async Task AgregarProductoSP(Producto producto)
        {
            var sql = "dbo.sp_agregar_producto";
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                cmd.Parameters.AddWithValue("@Estado", producto.Estado);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        // Función para listar los productos paginados con procedimiento almacenado
        public async Task<(List<Producto> productos, int totalRegistros)> ObtenerProductosPaginadosSP(int page, int pageSize)
        {
            var lista = new List<Producto>();
            var sql = "dbo.sp_listar_productos_paginados";
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Producto
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Precio = reader.GetDecimal(2),
                            Cantidad = reader.GetInt32(3),
                            Estado = reader.GetBoolean(4)
                        });
                    }
                    await reader.NextResultAsync();
                    int total = 0;
                    if (await reader.ReadAsync())
                    {
                        total = reader.GetInt32(0);
                    }
                    return (lista, total);
                }
            }
        }
    }
}
