using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Connections
{
    public class StudentConnection
    {
        private SQL _sql;

        public StudentConnection()
        {
            _sql = new SQL();
        }

        #region Create

        /// <summary>
        /// Creates new student in database
        /// </summary>
        /// <param name="student"></param>
        /// <param name="classId"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> CreateStudentAsync(Student student, int classId)
        {
            SqlCommand cmd = _sql.Execute("spCreateStudent");
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@email", student.Email);
            cmd.Parameters.AddWithValue("@password", student.Password);
            cmd.Parameters.AddWithValue("@class", classId);

            try
            {
                await cmd.Connection.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                await cmd.Connection.CloseAsync();
                return rowsAffected > 0;
            }
            catch ( SqlException ex )
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            finally
            {
                await cmd.Connection.CloseAsync();
            }

            return false;
        }

        #endregion

        #region Read

        /// <summary>
        /// Gets student from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Student if successful, null if not.</returns>
        public async Task<Student?> GetStudentAsync(int id)
        {
            SqlCommand cmd = _sql.Execute("spGetStudent");
            cmd.Parameters.AddWithValue("@id", id);

            Student student = null;

            try
            {
                await cmd.Connection.OpenAsync();

                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                if ( rdr.HasRows )
                {
                    while ( await rdr.ReadAsync() )
                    {
                        student = new Student(
                                (int)rdr["id"],
                                (string)rdr["name"],
                                (string)rdr["image"],
                                await rdr.IsDBNullAsync("description") ? null : (string)rdr["description"],
                                (string)rdr["email"],
                                (Specialities)rdr.GetByte("speciality"),
                                (int)rdr["fk_class"],
                               await rdr.IsDBNullAsync("end_date") ? null : new DateOnly(rdr.GetDateTime("end_date").Year, rdr.GetDateTime("end_date").Month, rdr.GetDateTime("end_date").Day),
                                (string)rdr["password"]
                        );
                    }

                    await rdr.CloseAsync();
                }

                return student;
            
            }
            catch ( SqlException ex )
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            finally
            {
                await cmd.Connection.CloseAsync();
            }

            return null;
        }

        /// <summary>
        /// Gets all students from database
        /// </summary>
        /// <returns>List<Student> if successful, null if not.</returns>
        public async Task<List<Student>?> GetStudentsAsync()
        {
            SqlCommand cmd = _sql.Execute("spGetStudents");

            List<Student> students = new List<Student>();

            try
            {
                await cmd.Connection.OpenAsync();

                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                if ( rdr.HasRows )
                {
                    while ( await rdr.ReadAsync() )
                    {
                        students.Add(new Student(
                                (int)rdr["id"],
                                (string)rdr["name"],
                                (string)rdr["image"],
                                await rdr.IsDBNullAsync("description") ? null : (string)rdr["description"],
                                (string)rdr["email"],
                                (Specialities)rdr.GetByte("speciality"),
                                (int)rdr["fk_class"],
                               await rdr.IsDBNullAsync("end_date") ? null : new DateOnly(rdr.GetDateTime("end_date").Year, rdr.GetDateTime("end_date").Month, rdr.GetDateTime("end_date").Day),
                                (string)rdr["password"]
                            ));
                    }

                    await rdr.CloseAsync();
                }

                return students;
            }
            catch ( SqlException ex )
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            finally
            {
                await cmd.Connection.CloseAsync();
            }

            return null;
        }

        /// <summary>
        /// Gets all students from database that contain the name given
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List<Student> if successful, null if not.</returns>
        public async Task<List<Student>?> GetStudentsByNameAsync(string name)
        {
            SqlCommand cmd = _sql.Execute("spGetStudentsByName");
            cmd.Parameters.AddWithValue("@name", name);

            List<Student> students = new List<Student>();

            try
            {
                await cmd.Connection.OpenAsync();

                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                if ( rdr.HasRows )
                {
                    while ( await rdr.ReadAsync() )
                    {
                        students.Add(new Student(
                                (int)rdr["id"],
                                (string)rdr["name"],
                                (string)rdr["image"],
                                await rdr.IsDBNullAsync("description") ? null : (string)rdr["description"],
                                (string)rdr["email"],
                                (Specialities)rdr.GetByte("speciality"),
                                (int)rdr["fk_class"],
                               await rdr.IsDBNullAsync("end_date") ? null : new DateOnly(rdr.GetDateTime("end_date").Year, rdr.GetDateTime("end_date").Month, rdr.GetDateTime("end_date").Day),
                                (string)rdr["password"]
                            ));
                    }

                    await rdr.CloseAsync();
                }

                return students;
            }
            catch ( SqlException ex )
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            finally
            {
                await cmd.Connection.CloseAsync();
            }

            return null;
        }

        /// <summary>
        /// Gets the student with the email given
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Student if successful, null if not.</returns>
        public async Task<Student?> GetStudentByEmailAsync(string email)
        {
            SqlCommand cmd = _sql.Execute("spGetStudentByEmail");
            cmd.Parameters.AddWithValue("@email", email);

            Student student = null;

            try
            {
                await cmd.Connection.OpenAsync();

                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                if ( rdr.HasRows )
                {
                    while ( await rdr.ReadAsync() )
                    {
                        student = new Student(
                                (int)rdr["id"],
                                (string)rdr["name"],
                                (string)rdr["image"],
                                await rdr.IsDBNullAsync("description") ? null : (string)rdr["description"],
                                (string)rdr["email"],
                                (Specialities)rdr.GetByte("speciality"),
                                (int)rdr["fk_class"],
                               await rdr.IsDBNullAsync("end_date") ? null : new DateOnly(rdr.GetDateTime("end_date").Year, rdr.GetDateTime("end_date").Month, rdr.GetDateTime("end_date").Day),
                                (string)rdr["password"]
                            );
                    }

                    await rdr.CloseAsync();
                }

                return student;

            }
            catch ( SqlException ex )
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            finally
            {
                await cmd.Connection.CloseAsync();
            }

            return null;
        }

        /// <summary>
        /// Gets all students from database that have the speciality given
        /// </summary>
        /// <param name="speciality"></param>
        /// <returns>List<Student> if successful, null if not.</returns>
        public async Task<List<Student>?> GetStudentsBySpecialityAsync(string speciality)
        {
            SqlCommand cmd = _sql.Execute("spGetStudentBySpeciality");
            cmd.Parameters.AddWithValue("@speciality", speciality);

            List<Student> students = new List<Student>();

            try
            {
                await cmd.Connection.OpenAsync();

                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                if ( rdr.HasRows )
                {
                    while ( await rdr.ReadAsync() )
                    {
                        students.Add( new Student(
                                (int)rdr["id"],
                                (string)rdr["name"],
                                (string)rdr["image"],
                                await rdr.IsDBNullAsync("description") ? null : (string)rdr["description"],
                                (string)rdr["email"],
                                (Specialities)rdr.GetByte("speciality"),
                                (int)rdr["fk_class"],
                               await rdr.IsDBNullAsync("end_date") ? null : new DateOnly(rdr.GetDateTime("end_date").Year, rdr.GetDateTime("end_date").Month, rdr.GetDateTime("end_date").Day),
                                (string)rdr["password"]
                            ));
                    }

                    await rdr.CloseAsync();
                }

                return students;
            }
            catch ( SqlException ex )
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            finally
            {
                await cmd.Connection.CloseAsync();
            }

            return null;
        }

        /// <summary>
        /// Gets all students from database that are in the class given
        /// </summary>
        /// <param name="class"></param>
        /// <returns>List<Student> if successful, null if not.</returns>
        public async Task<List<Student>?> GetStudentsByClassAsync(int id)
        {
            SqlCommand cmd = _sql.Execute("spGetStudentsByClass");
            cmd.Parameters.AddWithValue("@id", id);

            List<Student> students = new List<Student>();

            try
            {
                await cmd.Connection.OpenAsync();

                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                if ( rdr.HasRows )
                {
                    while ( await rdr.ReadAsync() )
                    {
                        students.Add(new Student(
                                (int)rdr["id"],
                                (string)rdr["name"],
                                (string)rdr["image"],
                                await rdr.IsDBNullAsync("description") ? null : (string)rdr["description"],
                                (string)rdr["email"],
                                (Specialities)rdr.GetByte("speciality"),
                                (int)rdr["fk_class"],
                               await rdr.IsDBNullAsync("end_date") ? null : new DateOnly(rdr.GetDateTime("end_date").Year, rdr.GetDateTime("end_date").Month, rdr.GetDateTime("end_date").Day),
                                (string)rdr["password"]
                            ));
                    }

                    await rdr.CloseAsync();
                }

                return students;
            }
            catch ( SqlException ex )
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            finally
            {
                await cmd.Connection.CloseAsync();
            }
            
            return null;
        }

        /// <summary>
        /// Get the most recent students from the database
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>List<Student>?</returns>
        public async Task<List<Student>?> GetLatestStudentsAsync(int amount = 5)
        {
            SqlCommand cmd = _sql.Execute("spGetLatestStudents");
            cmd.Parameters.AddWithValue("@amount", amount);

            List<Student> students = new List<Student>();

            try
            {
                await cmd.Connection.OpenAsync();

                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                if ( rdr.HasRows )
                {
                    while ( await rdr.ReadAsync() )
                    {
                        students.Add(new Student(
                                (int)rdr["id"],
                                (string)rdr["name"],
                                (string)rdr["image"],
                                await rdr.IsDBNullAsync("description") ? null : (string)rdr["description"],
                                (string)rdr["email"],
                                (Specialities)rdr.GetByte("speciality"),
                                (int)rdr["fk_class"],
                               await rdr.IsDBNullAsync("end_date") ? null : new DateOnly(rdr.GetDateTime("end_date").Year, rdr.GetDateTime("end_date").Month, rdr.GetDateTime("end_date").Day),
                                (string)rdr["password"]
                            ));
                    }

                    await rdr.CloseAsync();
                }

                return students;
            }
            catch ( SqlException ex )
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            finally
            {
                await cmd.Connection.CloseAsync();
            }

            return null;
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the student given
        /// </summary>
        /// <param name="student"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> UpdateStudentAsync(Student student)
        {
            SqlCommand cmd = _sql.Execute("spUpdateStudent");
            cmd.Parameters.AddWithValue("@id", student.Id);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@image", student.Image);
            cmd.Parameters.AddWithValue("@description", student.Description);
            cmd.Parameters.AddWithValue("@email", student.Email);
            cmd.Parameters.AddWithValue("@speciality", student.Speciality);
            cmd.Parameters.AddWithValue("@fk_class", student.ClassId);
            cmd.Parameters.AddWithValue("@end_date", student.EndDate == null ? null : student.EndDate.Value.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@password", student.Password);

            try
            {
                await cmd.Connection.OpenAsync();

                int rows = await cmd.ExecuteNonQueryAsync();

                return rows > 0;
            }
            catch ( SqlException ex )
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            finally
            {
                await cmd.Connection.CloseAsync();
            }

            return false;
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the student with the id given
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if not.</returns>
        public async Task<bool> DeleteStudentAsync(int id)
        {
            SqlCommand cmd = _sql.Execute("spDeleteStudent");
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                await cmd.Connection.OpenAsync();

                int rows = await cmd.ExecuteNonQueryAsync();

                return rows > 0;
            }
            catch ( SqlException ex )
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            finally
            {
                await cmd.Connection.CloseAsync();
            }

            return false;
        }
        #endregion

        #region Other

        /// <summary>
        /// Gets the number of students in the database
        /// </summary>
        /// <returns>int</returns>
        public async Task<int> GetStudentsCountAsync()
        {
            try
            {
                int count = 0;

                SqlCommand sqlCommand = _sql.Execute("spGetStudentsCount");
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

        public async Task<Dictionary<Specialities, int>?> GetStudentsCountGroupedBySpecialityAsync()
        {
            SqlCommand sqlCommand = _sql.Execute("spGetStudentsCountGroupedBySpeciality");

            try
            {
                await sqlCommand.Connection.OpenAsync();

                Dictionary<Specialities, int> studentsCount = new Dictionary<Specialities, int>();

                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                if ( sqlDataReader.HasRows )
                {
                    while ( await sqlDataReader.ReadAsync() )
                    {
                        studentsCount.Add((Specialities)sqlDataReader.GetByte("speciality"), sqlDataReader.GetInt32("count"));
                    }
                }

                await sqlDataReader.CloseAsync();
                await sqlCommand.Connection.CloseAsync();

                return studentsCount;
            }
            catch ( SqlException sqlException )
            {
                await Console.Out.WriteLineAsync(sqlException.Message);
            }

            return null;
        }

        #endregion
    }
}
