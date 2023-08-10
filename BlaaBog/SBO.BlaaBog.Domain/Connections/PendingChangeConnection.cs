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
    public class PendingChangeConnection
    {
        private SQL _sql;

        public PendingChangeConnection()
        {
            _sql = new SQL();
        }

        #region Create Pending Change

        /// <summary>
        /// Create a pending change in the database
        /// </summary>
        /// <param name="pendingChange"></param>
        /// <returns></returns>
        public async Task<bool> CreatePendingChangeAsync(PendingChange pendingChange)
        {
            SqlCommand sqlCommand = _sql.Execute("spCreatePendingChange");
            sqlCommand.Parameters.AddWithValue("@fk_student", pendingChange.StudentId);
            sqlCommand.Parameters.AddWithValue("@name", pendingChange.Name);
            sqlCommand.Parameters.AddWithValue("@image", pendingChange.Image);
            sqlCommand.Parameters.AddWithValue("@description", pendingChange.Description);

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

        #region Read Pending Change

        /// <summary>
        /// Get a specific pending change from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PendingChange?> GetPendingChangeAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spGetPendingChange");
            sqlCommand.Parameters.AddWithValue("@id", id);
            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                PendingChange? pendingChange = null;

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        pendingChange = new PendingChange(
                                sqlDataReader.GetInt32("id"),
                                sqlDataReader.GetInt32("fk_student"),
                                sqlDataReader.GetString("name"),
                                sqlDataReader.GetString("image"),
                                sqlDataReader.GetString("description")
                            );
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return pendingChange;
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
        /// Get all pending changes from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<PendingChange>?> GetPendingChangesAsync()
        {
            SqlCommand sqlCommand = _sql.Execute("spGetPendingChanges");

            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                List<PendingChange>? pendingChanges = null;

                if ( sqlDataReader.HasRows )
                {
                    pendingChanges = new List<PendingChange>();

                    while ( await sqlDataReader.ReadAsync() )
                    {
                        pendingChanges.Add(new PendingChange(
                                sqlDataReader.GetInt32("id"),
                                sqlDataReader.GetInt32("fk_student"),
                                sqlDataReader.GetString("name"),
                                sqlDataReader.GetString("image"),
                                sqlDataReader.GetString("description")
                            ));
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return pendingChanges;
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

        #endregion

        #region Update Pending Change

        #endregion

        #region Delete Pending Change

        /// <summary>
        /// Delete a pending change from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeletePendingChangeAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spDeletePendingChange");
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
