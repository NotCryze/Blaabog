using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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

        #region Create Class

        /// <summary>
        /// Create a class in the database
        /// </summary>
        /// <param name="class"></param>
        /// <returns></returns>
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

        #endregion

        #region Read Class

        /// <summary>
        /// Get a specific class from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Class?> GetClassAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spGetClass");
            sqlCommand.Parameters.AddWithValue("@id", id);
            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                Class @class = null;

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        @class = new Class(
                                sqlDataReader.GetInt32("id"),
                                new DateOnly(sqlDataReader.GetDateTime("start_date").Year, sqlDataReader.GetDateTime("start_date").Month, sqlDataReader.GetDateTime("start_date").Day),
                                sqlDataReader.GetString("token")
                            );
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return @class;
            }
            catch ( SqlException exception )
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }
            finally
            {
                await sqlCommand.Connection.CloseAsync();
            }

            return null;
        }

        /// <summary>
        /// Get all classes from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Class>?> GetClassesAsync()
        {
            SqlCommand sqlCommand = _sql.Execute("spGetClasses");
            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                List<Class> @class = new List<Class>();

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        @class.Add(new Class(
                                sqlDataReader.GetInt32("id"),
                                new DateOnly(
                                        sqlDataReader.GetDateTime("start_date").Year,
                                        sqlDataReader.GetDateTime("start_date").Month,
                                        sqlDataReader.GetDateTime("start_date").Day
                                    ),
                                sqlDataReader.GetString("token"))
                            );
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return @class;
            }
            catch ( SqlException exception )
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }
            finally
            {
                await sqlCommand.Connection.CloseAsync();
            }

            return new List<Class>();
        }

        #endregion

        #region Update Class

        /// <summary>
        /// Update a class in the database
        /// </summary>
        /// <param name="class"></param>
        /// <returns></returns>
        public async Task<bool> UpdateClassAsync(Class @class)
        {
            SqlCommand sqlCommand = _sql.Execute("spUpdateClass");
            sqlCommand.Parameters.AddWithValue("@id", @class.Id);
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

        #endregion

        #region Delete Class

        /// <summary>
        /// Delete a class from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteClassAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spDeleteClass");
            sqlCommand.Parameters.AddWithValue("@id", id);
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

        #endregion
    }
}
