using SBO.BlaaBog.Domain.Connections;
using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Services.Services
{
    public class StudentService
    {

        private StudentConnection _studentConnection;
        public StudentService()
        {
            _studentConnection = new StudentConnection();
        }

        #region Create

        /// <summary>
        /// Creates a student in the database
        /// </summary>
        /// <param name="student"></param>
        /// <param name="classId"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> CreateStudentAsync(Student student, int classId)
        {
            return await _studentConnection.CreateStudentAsync(student, classId);
        }

        #endregion

        #region Read

        /// <summary>
        /// Gets a student from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Student if successful, null if not.</returns>
        public async Task<Student?> GetStudentAsync(int id)
        {
            return await _studentConnection.GetStudentAsync(id);
        }

        /// <summary>
        /// Gets all students from the database
        /// </summary>
        /// <returns>List<Student> if successful, null if not.</returns>
        public async Task<List<Student>?> GetStudentsAsync()
        {
            return await _studentConnection.GetStudentsAsync();
        }

        /// <summary>
        /// Gets all students from the database that contain the name given
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List<Student> if successful, null if not.</returns>
        public async Task<List<Student>?> GetStudentsByNameAsync(string name)
        {
            return await _studentConnection.GetStudentsByNameAsync(name);
        }

        /// <summary>
        /// Gets a student from the database by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Student if successful, null if not.</returns>
        public async Task<Student?> GetStudentByEmailAsync(string email)
        {
            return await _studentConnection.GetStudentByEmailAsync(email);
        }

        /// <summary>
        /// Gets all students from the database by speciality
        /// </summary>
        /// <param name="speciality"></param>
        /// <returns>List<Student> if successful, null if not.</returns>
        public async Task<List<Student>?> GetStudentsbySpecialityAsync(string speciality)
        {
            return await _studentConnection.GetStudentsBySpecialityAsync(speciality);
        }

        /// <summary>
        /// Gets all students from the database by class
        /// </summary>
        /// <param name="class"></param>
        /// <returns>List<Student> if successful, null if not.</returns>
        public async Task<List<Student>?> GetStudentsByClassAsync(int @class)
        {
            return await _studentConnection.GetStudentsByClassAsync(@class);
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates a student in the database
        /// </summary>
        /// <param name="student"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> UpdateStudentAsync(Student student)
        {
            return await _studentConnection.UpdateStudentAsync(student);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes a student from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentConnection.DeleteStudentAsync(id);
        }

        #endregion


    }
}
