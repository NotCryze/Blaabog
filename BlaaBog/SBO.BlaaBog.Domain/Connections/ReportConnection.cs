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
    public class ReportConnection
    {
        private SQL _sql;

        public ReportConnection()
        {
            _sql = new SQL();
        }

        #region Create Report

        public async Task<bool> CreateReportAsync(Report report)
        {
            SqlCommand sqlCommand = _sql.Execute("spCreateReport");
            sqlCommand.Parameters.AddWithValue("@fk_comment", report.CommentId);
            sqlCommand.Parameters.AddWithValue("@reason", report.Reason);

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

        #region Read Report

        public async Task<Report?> GetReportAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spGetReport");
            sqlCommand.Parameters.AddWithValue("@id", id);
            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                Report? report = null;

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        report = new Report(
                                sqlDataReader.GetInt32("id"),
                                sqlDataReader.GetInt32("fk_comment"),
                                sqlDataReader.GetString("reason"),
                                sqlDataReader.GetDateTime("created_at")
                            );
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return report;
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

        public async Task<List<Report>?> GetReportsAsync()
        {
            SqlCommand sqlCommand = _sql.Execute("spGetReports");

            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                List<Report> reports = new List<Report>();

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        reports.Add(new Report(
                                sqlDataReader.GetInt32("id"),
                                sqlDataReader.GetInt32("fk_comment"),
                                sqlDataReader.GetString("reason"),
                                sqlDataReader.GetDateTime("created_at")
                            ));
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return reports;
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

        #region Update Report

        #endregion

        #region Delete Report

        public async Task<bool> DeleteReportAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spDeleteReport");
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
