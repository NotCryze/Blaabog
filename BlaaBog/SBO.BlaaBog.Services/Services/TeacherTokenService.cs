using SBO.BlaaBog.Domain.Connections;
using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Services.Services
{
    public class TeacherTokenService
    {
        private TeacherTokenConnection _teacherTokenConnection;
        public TeacherTokenService()
        {
            _teacherTokenConnection = new TeacherTokenConnection();
        }

        #region Create

        /// <summary>
        /// Creates a teacher token in the database
        /// </summary>
        /// <param name="teacherToken"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> CreateTeacherTokenAsync(TeacherToken teacherToken)
        {
            return await _teacherTokenConnection.CreateTeacherTokenAsync(teacherToken);
        }

        #endregion

        #region Read

        /// <summary>
        /// Gets a teacher token from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TeacherToken if successful, null if not.</returns>
        public async Task<TeacherToken?> GetTeacherTokenAsync(int id)
        {
            return await _teacherTokenConnection.GetTeacherTokenAsync(id);
        }

        /// <summary>
        /// Gets all teacher tokens from the database
        /// </summary>
        /// <returns>List<TeacherToken> if successful, null if not.</returns>
        public async Task<List<TeacherToken>?> GetTeacherTokensAsync()
        {
            return await _teacherTokenConnection.GetTeacherTokensAsync();
        }

        /// <summary>
        /// Gets a teacher token from the database by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>TeacherToken if successful, null if not.</returns>
        public async Task<TeacherToken?> GetTeacherTokenByTokenAsync(string token)
        {
            return await _teacherTokenConnection.GetTeacherTokenByTokenAsync(token);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes a teacher token from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> DeleteTeacherTokenAsync(int id)
        {
            return await _teacherTokenConnection.DeleteTeacherTokenAsync(id);
        }

        #endregion


        #region Use

        /// <summary>
        /// Uses a teacher token in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teacherId"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> UseTeacherTokenAsync(int id, int teacherId)
        {
            return await _teacherTokenConnection.UseTeacherTokenAsync(id, teacherId);
        }

        #endregion
    }
}
