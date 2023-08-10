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
    public class CommentConnection
    {
        private SQL _sql;

        public CommentConnection()
        {
            _sql = new SQL();
        }

        #region Create Comment

        /// <summary>
        /// Create a comment in the database
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>bool</returns>
        public async Task<bool> CreateCommentAsync(Comment comment)
        {
            SqlCommand sqlCommand = _sql.Execute("spCreateComment");
            sqlCommand.Parameters.AddWithValue("@fk_author", comment.AuthorId);
            sqlCommand.Parameters.AddWithValue("@fk_subject", comment.SubjectId);
            sqlCommand.Parameters.AddWithValue("@content", comment.Content);
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

        #region Read Comment


        /// <summary>
        /// Get a specific comment from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Comment?</returns>
        public async Task<Comment?> GetCommentAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spGetComment");
            sqlCommand.Parameters.AddWithValue("@id", id);
            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                Comment? comment = null;

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        comment = new Comment(
                                sqlDataReader.GetInt32("id"),
                                sqlDataReader.GetInt32("fk_author"),
                                sqlDataReader.GetInt32("fk_subject"),
                                sqlDataReader.GetString("content"),
                                sqlDataReader.GetBoolean("approved"),
                                sqlDataReader.GetInt32("approved_by"),
                                sqlDataReader.GetDateTime("approved_at"),
                                sqlDataReader.GetDateTime("created_at")
                            );
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return comment;
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
        /// Get all comments from the database
        /// </summary>
        /// <returns>List<Comment>?</returns>
        public async Task<List<Comment>?> GetCommentsAsync()
        {
            SqlCommand sqlCommand = _sql.Execute("spGetComment");

            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                List<Comment> comments = new List<Comment>();

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        comments.Add(new Comment(
                                sqlDataReader.GetInt32("id"),
                                sqlDataReader.GetInt32("fk_author"),
                                sqlDataReader.GetInt32("fk_subject"),
                                sqlDataReader.GetString("content"),
                                sqlDataReader.GetBoolean("approved"),
                                sqlDataReader.GetInt32("approved_by"),
                                sqlDataReader.GetDateTime("approved_at"),
                                sqlDataReader.GetDateTime("created_at")
                            ));
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return comments;
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
        /// Get all non approved comments from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<Comment>?</returns>
        public async Task<List<Comment>?> GetNonApprovedCommentsAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spGetNonApprovedComments");
            sqlCommand.Parameters.AddWithValue("@id", id);

            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                List<Comment> comments = new List<Comment>();

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        comments.Add(new Comment(
                                sqlDataReader.GetInt32("id"),
                                sqlDataReader.GetInt32("fk_author"),
                                sqlDataReader.GetInt32("fk_subject"),
                                sqlDataReader.GetString("content"),
                                sqlDataReader.GetBoolean("approved"),
                                sqlDataReader.GetInt32("approved_by"),
                                sqlDataReader.GetDateTime("approved_at"),
                                sqlDataReader.GetDateTime("created_at")
                            ));
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return comments;
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
        /// Get all comments from a specific author
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<Comment>?</returns>
        public async Task<List<Comment>?> GetCommentsByAuthorAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spGetCommentsByAuthor");
            sqlCommand.Parameters.AddWithValue("@id", id);

            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                List<Comment> comments = new List<Comment>();

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        comments.Add(new Comment(
                                sqlDataReader.GetInt32("id"),
                                sqlDataReader.GetInt32("fk_author"),
                                sqlDataReader.GetInt32("fk_subject"),
                                sqlDataReader.GetString("content"),
                                sqlDataReader.GetBoolean("approved"),
                                sqlDataReader.GetInt32("approved_by"),
                                sqlDataReader.GetDateTime("approved_at"),
                                sqlDataReader.GetDateTime("created_at")
                            ));
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return comments;
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
        /// Get all comments from a specific subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<Comment>?</returns>
        public async Task<List<Comment>?> GetCommentsBySubjectAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spGetCommentsBySubject");
            sqlCommand.Parameters.AddWithValue("@id", id);

            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                List<Comment> comments = new List<Comment>();

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        comments.Add(new Comment(
                                sqlDataReader.GetInt32("id"),
                                sqlDataReader.GetInt32("fk_author"),
                                sqlDataReader.GetInt32("fk_subject"),
                                sqlDataReader.GetString("content"),
                                sqlDataReader.GetBoolean("approved"),
                                sqlDataReader.GetInt32("approved_by"),
                                sqlDataReader.GetDateTime("approved_at"),
                                sqlDataReader.GetDateTime("created_at")
                            ));
                    }

                    await sqlDataReader.CloseAsync();
                }

                await sqlCommand.Connection.CloseAsync();

                return comments;
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

        #region Update Comment

        /// <summary>
        /// Update a comment in the database
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateCommentAsync(Comment comment)
        {
            SqlCommand sqlCommand = _sql.Execute("spUpdateComment");
            sqlCommand.Parameters.AddWithValue("@id", comment.Id);
            sqlCommand.Parameters.AddWithValue("@approved", comment.Approved);
            sqlCommand.Parameters.AddWithValue("@approved_by", comment.ApprovedBy);
            sqlCommand.Parameters.AddWithValue("@approved_at", comment.ApprovedAt);

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

        #region Delete Comment

        /// <summary>
        /// Delete a comment from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteCommentAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spDeleteComment");
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

        /// <summary>
        /// Approve a comment in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvedBy"></param>
        /// <returns>bool</returns>
        public async Task<bool> ApproveCommentAsync(int id, int approvedBy)
        {
            SqlCommand sqlCommand = _sql.Execute("spApproveComment");
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@approved_by", approvedBy);

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
