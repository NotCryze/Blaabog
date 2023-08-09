using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Connections
{
    public class ClassConnection
    {
        private SQL _sql;

        public ClassConnection()
        {
            _sql = new SQL();
        }

        public async Task<bool> CreateClassAsync(Class @class)
        {
            SqlCommand sqlCommand = _sql.Execute("spCreateClass");
            sqlCommand.Parameters.AddWithValue("@start_date", @class.StartDate);
            sqlCommand.Parameters.AddWithValue("@token", @class.Token);
            try
            {
                await sqlCommand.Connection.OpenAsync();
                int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();
                await sqlCommand.Connection.CloseAsync();
                return rowsAffected > 0;
            }
            catch ( SqlException exception )
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }
            finally
            {
                await sqlCommand.Connection.CloseAsync();
            }

            return false;
        }
    }
}
