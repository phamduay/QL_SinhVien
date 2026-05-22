using System.Data.SqlClient;

namespace QLSinhVien.Services
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString =
            "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QL_Sinhvien;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
