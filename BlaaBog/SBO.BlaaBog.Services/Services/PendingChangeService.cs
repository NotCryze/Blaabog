using SBO.BlaaBog.Domain.Connections;
using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Services.Services
{
    public class PendingChangeService
    {
        private PendingChangeConnection _pendingChangeConnection;

        public PendingChangeService()
        {
            _pendingChangeConnection = new PendingChangeConnection();
        }

        #region Create Pending Change

        /// <summary>
        /// Create a pending change in the database
        /// </summary>
        /// <param name="pendingChange"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> CreatePendingChangeAsync(PendingChange pendingChange)
        {
            return await _pendingChangeConnection.CreatePendingChangeAsync(pendingChange);
        }

        #endregion

        #region Read Pending Change

        /// <summary>
        /// Get a specific pending change from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>PendingChange if successful, null if not.</returns>
        public async Task<PendingChange?> GetPendingChangeAsync(int id)
        {
            return await _pendingChangeConnection.GetPendingChangeAsync(id);
        }

        /// <summary>
        /// Get all pending changes from the database
        /// </summary>
        /// <returns>PendingChange if successful, null if not.</returns>
        public async Task<List<PendingChange>?> GetPendingChangesAsync()
        {
            return await _pendingChangeConnection.GetPendingChangesAsync();
        }

        #region Update Pending Change

        #endregion

        #region Delete Pending Change

        /// <summary>
        /// Delete a pending change from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> DeletePendingChangeAsync(int id)
        {
            return await _pendingChangeConnection.DeletePendingChangeAsync(id);
        }

        #endregion
    }
}
