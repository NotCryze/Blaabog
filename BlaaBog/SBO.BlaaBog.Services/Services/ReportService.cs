using SBO.BlaaBog.Domain.Connections;
using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Services.Services
{
    public class ReportService
    {
        private ReportConnection _reportConnection;

        public ReportService()
        {
            _reportConnection = new ReportConnection();
        }

        #region Create Report

        /// <summary>
        /// Create a report in the database
        /// </summary>
        /// <param name="report"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> CreateReportAsync(Report report)
        {
            return await _reportConnection.CreateReportAsync(report);
        }

        #endregion

        #region Read Report

        /// <summary>
        /// Get a specific report from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Report if successful, null if not.</returns>
        public async Task<Report?> GetReportAsync(int id)
        {
            return await _reportConnection.GetReportAsync(id);
        }

        /// <summary>
        /// Get all reports from the database
        /// </summary>
        /// <returns>List<Report>?</returns>
        public async Task<List<Report>?> GetReportsAsync()
        {
            return await _reportConnection.GetReportsAsync();
        }

        #endregion

        #region Update Report

        #endregion

        #region Delete Report

        /// <summary>
        /// Delete a report from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> DeleteReportAsync(int id)
        {
            return await _reportConnection.DeleteReportAsync(id);
        }

        #endregion

        #region Other

        /// <summary>
        /// Gets the number of reports in the database
        /// </summary>
        /// <returns>int</returns>
        public async Task<int> GetReportsCountAsync()
        {
            return await _reportConnection.GetReportsCountAsync();
        }

        #endregion
    }
}
