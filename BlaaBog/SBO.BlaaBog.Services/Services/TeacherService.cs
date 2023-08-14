using SBO.BlaaBog.Domain.Connections;
using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Services.Services
{
    public class TeacherService
    {
        private TeacherConnection _teacherConnection;
        public TeacherService()
        {
            _teacherConnection = new TeacherConnection();
        }

        #region Create

        /// <summary>
        /// Creates a teacher in the database
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> CreateTeacherAsync(Teacher teacher)
        {
            return await _teacherConnection.CreateTeacherAsync(teacher);
        }

        #endregion

        #region Read

        /// <summary>
        /// Gets a teacher from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Teacher if successful, null if not.</returns>
        public async Task<Teacher?> GetTeacherAsync(int id)
        {
            return await _teacherConnection.GetTeacherAsync(id);
        }

        /// <summary>
        /// Gets all teachers from the database
        /// </summary>
        /// <returns>List<Teacher> if successful, null if not.</returns>
        public async Task<List<Teacher>?> GetTeachersAsync()
        {
            return await _teacherConnection.GetTeachersAsync();
        }

        /// <summary>
        /// Gets all teachers from the database that contain the name given
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List<Teacher> if successful, null if not.</returns>
        public async Task<List<Teacher>?> GetTeachersByNameAsync(string name)
        {
            return await _teacherConnection.GetTeachersByNameAsync(name);
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates a teacher in the database
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> UpdateTeacherAsync(Teacher teacher)
        {
            return await _teacherConnection.UpdateTeacherAsync(teacher);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes a teacher from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> DeleteTeacherAsync(int id)
        {
            return await _teacherConnection.DeleteTeacherAsync(id);
        }

        #endregion
    }
}
