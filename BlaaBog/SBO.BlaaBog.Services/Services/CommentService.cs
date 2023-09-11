using SBO.BlaaBog.Domain.Connections;
using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Services.Services
{
    public class CommentService
    {
        private CommentConnection _commentConnection;

        public CommentService()
        {
            _commentConnection = new CommentConnection();
        }

        #region Create Comment

        /// <summary>
        /// Create a comment in the database
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> CreateCommentAsync(Comment comment)
        {
            return await _commentConnection.CreateCommentAsync(comment);
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
            return await _commentConnection.GetCommentAsync(id);
        }

        /// <summary>
        /// Get all comments from the database
        /// </summary>
        /// <returns>Comment if successful, null if not.</returns>
        public async Task<List<Comment>?> GetCommentsAsync()
        {
            return await _commentConnection.GetCommentsAsync();
        }

        /// <summary>
        /// Get all non approved comments from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Comment if successful, null if not.</returns>
        public async Task<List<Comment>?> GetNonApprovedCommentsAsync(int id)
        {
            return await _commentConnection.GetNonApprovedCommentsAsync(id);
        }

        /// <summary>
        /// Get all comments from a specific author
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Comment if successful, null if not.</returns>
        public async Task<List<Comment>?> GetCommentsByAuthorAsync(int id)
        {
            return await _commentConnection.GetCommentsByAuthorAsync(id);
        }

        /// <summary>
        /// Get all comments from a specific subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Comment if successful, null if not.</returns>
        public async Task<List<Comment>?> GetCommentsBySubjectAsync(int id)
        {
            return await _commentConnection.GetCommentsBySubjectAsync(id);
        }

        /// <summary>
        /// Get recent comments from the database
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>List<Comment>?</returns>
        public async Task<List<Comment>?> GetLatestCommentsAsync(int amount = 5)
        {
            return await _commentConnection.GetLatestCommentsAsync(amount);
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
            return await _commentConnection.UpdateCommentAsync(comment);
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
            return await _commentConnection.DeleteCommentAsync(id);
        }

        /// <summary>
        /// Deletes all comments from a specific author
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> DeleteCommentsByAuthorAsync(int id)
        {
            return await _commentConnection.DeleteCommentsByAuthorAsync(id);
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
            return await _commentConnection.ApproveCommentAsync(id, approvedBy);
        }

        /// <summary>
        /// Gets the number of students in the database
        /// </summary>
        /// <returns>int</returns>
        public async Task<int> GetCommentsCountAsync()
        {
            return await _commentConnection.GetCommentsCountAsync();
        }

        /// <summary>
        /// Gets the number of new comments in the database
        /// </summary>
        /// <returns>int</returns>
        public async Task<int> GetNewCommentsCountAsync()
        {
            return await _commentConnection.GetNewCommentsCountAsync();
        }

        /// <summary>
        /// Gets the number of comments in the database group by month
        /// </summary>
        /// <returns>Dictionary<DateOnly, int>?</returns>
        public async Task<Dictionary<DateOnly, int>> GetCommentsGroupedByMonthAsync()
        {
            return await _commentConnection.GetCommentsGroupedByMonthAsync();
        }

        #endregion
    }
}
