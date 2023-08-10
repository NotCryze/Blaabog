using SBO.BlaaBog.Domain.Connections;
using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
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

        #endregion
    }
}
