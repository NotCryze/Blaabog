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
        /// <returns>true if successful, false if not.</returns>
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
        /// <returns>Comment if successful, null if not.</returns>
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
                        comment = new Comment
                        {
                            Id = sqlDataReader.GetInt32("id"),
                            AuthorId = sqlDataReader.GetInt32("fk_author"),
                            SubjectId = sqlDataReader.GetInt32("fk_subject"),
                            Content = sqlDataReader.GetString("content"),
                            Approved = sqlDataReader.GetBoolean("approved"),
                            ApprovedById = await sqlDataReader.IsDBNullAsync("approved_by") ? null : await sqlDataReader.IsDBNullAsync("approved_by") ? null : sqlDataReader.GetInt32("approved_by"),
                            ApprovedAt = await sqlDataReader.IsDBNullAsync("approved_at") ? null : await sqlDataReader.IsDBNullAsync("approved_at") ? null : sqlDataReader.GetDateTime("approved_at"),
                            CreatedAt = sqlDataReader.GetDateTime("created_at")
                        };
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
        /// <returns>Comment if successful, null if not.</returns>
        public async Task<List<Comment>?> GetCommentsAsync()
        {
            SqlCommand sqlCommand = _sql.Execute("spGetComments");

            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                List<Comment> comments = new List<Comment>();

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        comments.Add(new Comment
                        {
                            Id = sqlDataReader.GetInt32("id"),
                            AuthorId = sqlDataReader.GetInt32("fk_author"),
                            SubjectId = sqlDataReader.GetInt32("fk_subject"),
                            Content = sqlDataReader.GetString("content"),
                            Approved = sqlDataReader.GetBoolean("approved"),
                            ApprovedById = await sqlDataReader.IsDBNullAsync("approved_by") ? null : sqlDataReader.GetInt32("approved_by"),
                            ApprovedAt = await sqlDataReader.IsDBNullAsync("approved_at") ? null : sqlDataReader.GetDateTime("approved_at"),
                            CreatedAt = sqlDataReader.GetDateTime("created_at")
                        });
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
        /// <returns>Comment if successful, null if not.</returns>
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
                        comments.Add(new Comment
                        {
                            Id = sqlDataReader.GetInt32("id"),
                            AuthorId = sqlDataReader.GetInt32("fk_author"),
                            SubjectId = sqlDataReader.GetInt32("fk_subject"),
                            Content = sqlDataReader.GetString("content"),
                            Approved = sqlDataReader.GetBoolean("approved"),
                            ApprovedById = await sqlDataReader.IsDBNullAsync("approved_by") ? null : sqlDataReader.GetInt32("approved_by"),
                            ApprovedAt = await sqlDataReader.IsDBNullAsync("approved_at") ? null : sqlDataReader.GetDateTime("approved_at"),
                            CreatedAt = sqlDataReader.GetDateTime("created_at")
                        });
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
        /// <returns>Comment if successful, null if not.</returns>
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
                        comments.Add(new Comment
                        {
                            Id = sqlDataReader.GetInt32("id"),
                            AuthorId = sqlDataReader.GetInt32("fk_author"),
                            SubjectId = sqlDataReader.GetInt32("fk_subject"),
                            Content = sqlDataReader.GetString("content"),
                            Approved = sqlDataReader.GetBoolean("approved"),
                            ApprovedById = await sqlDataReader.IsDBNullAsync("approved_by") ? null : sqlDataReader.GetInt32("approved_by"),
                            ApprovedAt = await sqlDataReader.IsDBNullAsync("approved_at") ? null : sqlDataReader.GetDateTime("approved_at"),
                            CreatedAt = sqlDataReader.GetDateTime("created_at")
                        });
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
        /// <returns>Comment if successful, null if not.</returns>
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
                        comments.Add(new Comment
                        {
                            Id = sqlDataReader.GetInt32("id"),
                            AuthorId = sqlDataReader.GetInt32("fk_author"),
                            SubjectId = sqlDataReader.GetInt32("fk_subject"),
                            Content = sqlDataReader.GetString("content"),
                            Approved = sqlDataReader.GetBoolean("approved"),
                            ApprovedById = await sqlDataReader.IsDBNullAsync("approved_by") ? null : sqlDataReader.GetInt32("approved_by"),
                            ApprovedAt = await sqlDataReader.IsDBNullAsync("approved_at") ? null : sqlDataReader.GetDateTime("approved_at"),
                            CreatedAt = sqlDataReader.GetDateTime("created_at")
                        });
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
        /// Get recent comments from the database
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>List<Comment>?</returns>
        public async Task<List<Comment>?> GetLatestCommentsAsync(int amount = 5)
        {
            SqlCommand sqlCommand = _sql.Execute("spGetLatestComments");
            sqlCommand.Parameters.AddWithValue("@amount", amount);

            try
            {
                await sqlCommand.Connection.OpenAsync();
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                List<Comment> comments = new List<Comment>();

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        comments.Add(new Comment
                        {
                            Id = sqlDataReader.GetInt32("id"),
                            AuthorId = sqlDataReader.GetInt32("fk_author"),
                            SubjectId = sqlDataReader.GetInt32("fk_subject"),
                            Content = sqlDataReader.GetString("content"),
                            Approved = sqlDataReader.GetBoolean("approved"),
                            ApprovedById = await sqlDataReader.IsDBNullAsync("approved_by") ? null : sqlDataReader.GetInt32("approved_by"),
                            ApprovedAt = await sqlDataReader.IsDBNullAsync("approved_at") ? null : sqlDataReader.GetDateTime("approved_at"),
                            CreatedAt = sqlDataReader.GetDateTime("created_at")
                        });
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
        /// <returns>true if successful, false if not.</returns>
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
        /// <returns>true if successful, false if not.</returns>
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

        /// <summary>
        /// Deletes all comments from a specific author
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> DeleteCommentsByAuthorAsync(int id)
        {
            SqlCommand sqlCommand = _sql.Execute("spDeleteCommentsByAuthor");
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
        /// <returns>true if successful, false if not.</returns>
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

        /// <summary>
        /// Gets the number of students in the database
        /// </summary>
        /// <returns>int</returns>
        public async Task<int> GetCommentsCountAsync()
        {
            try
            {
                int count = 0;

                SqlCommand sqlCommand = _sql.Execute("spGetCommentsCount");
                await sqlCommand.Connection.OpenAsync();
                count = Convert.ToInt32(await sqlCommand.ExecuteScalarAsync());
                await sqlCommand.Connection.CloseAsync();

                return count;
            }
            catch ( SqlException sqlException )
            {
                await Console.Out.WriteLineAsync(sqlException.Message);
            }

            return 0;
        }

        /// <summary>
        /// Gets the number of new comments in the database
        /// </summary>
        /// <returns>int</returns>
        public async Task<int> GetNewCommentsCountAsync()
        {
            try
            {
                int count = 0;

                SqlCommand sqlCommand = _sql.Execute("spGetNewCommentsCount");
                await sqlCommand.Connection.OpenAsync();
                count = Convert.ToInt32(await sqlCommand.ExecuteScalarAsync());
                await sqlCommand.Connection.CloseAsync();

                return count;
            }
            catch ( SqlException sqlException )
            {
                await Console.Out.WriteLineAsync(sqlException.Message);
            }

            return 0;
        }

        /// <summary>
        /// Gets comments grouped by month over the last 5 years
        /// </summary>
        /// <returns>Dictionary<DateOnly, int>?</returns>
        public async Task<Dictionary<DateOnly, int>?> GetCommentsGroupedByMonthAsync()
        {
            SqlCommand sqlCommand = _sql.Execute("spGetCommentsGroupedByMonth");

            try
            {
                await sqlCommand.Connection.OpenAsync();
                
                Dictionary<DateOnly, int> comments = new Dictionary<DateOnly, int>();

                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                while ( sqlDataReader.Read() )
                {
                    comments.Add(new DateOnly(sqlDataReader.GetDateTime("date").Year, sqlDataReader.GetDateTime("date").Month, sqlDataReader.GetDateTime("date").Day), sqlDataReader.GetInt32("count"));
                }

                await sqlDataReader.CloseAsync();
                await sqlCommand.Connection.CloseAsync();

                return comments;
            }
            catch ( SqlException sqlException )
            {
                await Console.Out.WriteLineAsync(sqlException.Message);
            }
            finally
            {
                await sqlCommand.Connection.CloseAsync();
            }

            return null;
        }

        #endregion
    }
}
