using SBO.BlaaBog.Domain.Connections;
using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Services.Services
{
    public class ClassService
    {
        private ClassConnection _classConnection;

        public ClassService()
        {
            _classConnection = new ClassConnection();
        }

        #region Create Class

        /// <summary>
        /// Create a class in the database
        /// </summary>
        /// <param name="class"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> CreateClassAsync(Class @class)
        {
            return await _classConnection.CreateClassAsync(@class);
        }

        #endregion

        #region Read Class

        /// <summary>
        /// Get a specific class from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Class if successful, null if not.</returns>
        public async Task<Class?> GetClassAsync(int id)
        {
            return await _classConnection.GetClassAsync(id);
        }

        /// <summary>
        /// Get all classes from the database
        /// </summary>
        /// <returns>List<Class> if successful, null if not.</returns>
        public async Task<List<Class>?> GetClassesAsync()
        {
            return await _classConnection.GetClassesAsync();
        }

        #endregion

        #region Update Class

        /// <summary>
        /// Update a class in the database
        /// </summary>
        /// <param name="class"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> UpdateClassAsync(Class @class)
        {
            return await _classConnection.UpdateClassAsync(@class);
        }

        #endregion

        #region Delete Class

        /// <summary>
        /// Delete a class from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> DeleteClassAsync(int id)
        {
            return await _classConnection.DeleteClassAsync(id);
        }

        #endregion
    }
}
