using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Connections
{
    public class TeacherTokenConnection
    {
        private SQL _sql;

        public TeacherTokenConnection()
        {
            _sql = new SQL();
        }

        #region Create Teacher Token

        public async Task<bool> CreateTeacherTokenAsync(TeacherToken teacherToken)
        {
            SqlCommand sqlCommand = _sql.Execute("spCreateTeacherToken");
            sqlCommand.Parameters.AddWithValue("@token", teacherToken.Token);
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

        #region Read Teacher Token

        public async Task<TeacherToken?> GetTeacherTokenAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spCreateTeacherToken");
            sqlCommand.Parameters.AddWithValue("@id", id);
            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                TeacherToken? teacherToken = null;

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        teacherToken = new TeacherToken {
                               Id = sqlDataReader.GetInt32("id"),
                                Token = sqlDataReader.GetString("token"),
                                 TeacherId = await sqlDataReader.IsDBNullAsync("fk_teacher") ? null : sqlDataReader.GetInt32("fk_teacher"),
                                CreatedAt = sqlDataReader.GetDateTime("created_at")
                            };
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return teacherToken;
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

        public async Task<List<TeacherToken>> GetTeacherTokensAsync()
        {
            SqlCommand sqlCommand = _sql.Execute("spGetTeacherTokens");
            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                List<TeacherToken> teacherTokens = new List<TeacherToken>();

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        teacherTokens.Add(new TeacherToken { 
                               Id =  sqlDataReader.GetInt32("id"),
                                Token = sqlDataReader.GetString("token"),
                                TeacherId = await sqlDataReader.IsDBNullAsync("fk_teacher") ? null : sqlDataReader.GetInt32("fk_teacher"),
                                CreatedAt = sqlDataReader.GetDateTime("created_at")
                             });
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return teacherTokens;
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

        public async Task<TeacherToken?> GetTeacherTokenByTokenAsync(string token)
        {
            SqlCommand sqlCommand = _sql.Execute("spGetTeacherTokenByToken");
            sqlCommand.Parameters.AddWithValue("@token", token);
            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                TeacherToken? teacherToken = null;

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        teacherToken = new TeacherToken {
                               Id = sqlDataReader.GetInt32("id"),
                                Token = sqlDataReader.GetString("token"),
                                TeacherId = await sqlDataReader.IsDBNullAsync("fk_teacher") ? null : sqlDataReader.GetInt32("fk_teacher"),
                                CreatedAt = sqlDataReader.GetDateTime("created_at")
                            };
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return teacherToken;
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

        #region Update Teacher Token

        #endregion

        #region Delete Teacher Token

        public async Task<bool> DeleteTeacherTokenAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spDeleteTeacherToken");
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

        #region Other

        public async Task<bool> UseTeacherTokenAsync(int id, int teacherId)
        {
            SqlCommand sqlCommand = _sql.Execute("spUseTeacherToken");
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@teacher", teacherId);
            try
            {
                await sqlCommand.Connection.OpenAsync();
                int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();
                await sqlCommand.Connection.CloseAsync();
                return rowsAffected > 0;
            }
            catch ( SqlException sqlException )
            {
                await Console.Out.WriteLineAsync(sqlException.Message);
            }

            return false;
        }

        #endregion
    }
}
