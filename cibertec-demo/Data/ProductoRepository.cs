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
    }
}
