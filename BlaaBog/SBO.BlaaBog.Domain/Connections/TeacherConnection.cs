using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Connections
{
    public class TeacherConnection
    {
        private SQL _sql;

        public TeacherConnection()
        {
            _sql = new SQL();
        }

        #region Create

        /// <summary>
        /// Creates new teacher in database
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns> bool </returns>
        public async Task<bool> CreateTeacherAsync(Teacher teacher)
        {
            SqlCommand cmd = _sql.Execute("spCreateTeacher");
            cmd.Parameters.AddWithValue("@name", teacher.Name);
            cmd.Parameters.AddWithValue("@email", teacher.Email);
            cmd.Parameters.AddWithValue("@password", teacher.Password);
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
        /// Gets a specific teacher from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Teacher </returns>
        public async Task<Teacher?> GetTeacherAsync(int id)
        {
            SqlCommand cmd = _sql.Execute("spGetTeacher");
            cmd.Parameters.AddWithValue("@id", id);
            Teacher teacher = null;
            try
            {
                await cmd.Connection.OpenAsync();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                if ( rdr.HasRows )
                {
                    while ( await rdr.ReadAsync() )
                    {
                        teacher = new Teacher(
                        (int)rdr["id"],
                        (string)rdr["name"],
                        (string)rdr["email"],
                        (string)rdr["password"],
                        (bool)rdr["admin"]
                        );
                    }

                    await rdr.CloseAsync(); 
                }
                await cmd.Connection.CloseAsync();

                return teacher;
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
        /// Gets all teachers from database
        /// </summary>
        /// <returns> List<Teacher> </returns>
        public async Task<List<Teacher>?> GetTeachersAsync()
        {
            SqlCommand cmd = _sql.Execute("spGetTeachers");
            try
            {
                await cmd.Connection.OpenAsync();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                List<Teacher> teachers = new List<Teacher>();
                if ( rdr.HasRows )
                {
                    while ( await rdr.ReadAsync() )
                    {
                        teachers.Add(new Teacher(
                                (int)rdr["id"],
                                (string)rdr["name"],
                                (string)rdr["email"],
                                (string)rdr["password"],
                                (bool)rdr["admin"]
                            ));
                    }

                    await rdr.CloseAsync();
                }
                await cmd.Connection.CloseAsync();
                return teachers;
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
        /// Gets a list of teachers that contain the name given
        /// </summary>
        /// <param name="name"></param>
        /// <returns> List<Teacher> </returns>
        public async Task<List<Teacher>?> GetTeachersByNameAsync(string name)
        {
            SqlCommand cmd = _sql.Execute("spGetTeachersByName");
            cmd.Parameters.AddWithValue("@name", name);
            List<Teacher> teachers = new List<Teacher>();
            try
            {
                await cmd.Connection.OpenAsync();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                if ( rdr.HasRows )
                {
                    while ( await rdr.ReadAsync() )
                    {
                        teachers.Add(new Teacher(
                                (int)rdr["id"],
                                (string)rdr["name"],
                                (string)rdr["email"],
                                (string)rdr["password"],
                                (bool)rdr["admin"]
                            ));
                    }

                    await rdr.CloseAsync();
                }
                await cmd.Connection.CloseAsync();
                return teachers;
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
        /// Updates a teacher in database
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns> bool </returns>
        public async Task<bool> UpdateTeacherAsync(Teacher teacher)
        {
            SqlCommand cmd = _sql.Execute("spUpdateTeacger");
            cmd.Parameters.AddWithValue("@id", teacher.Id);
            cmd.Parameters.AddWithValue("@name", teacher.Name);
            cmd.Parameters.AddWithValue("@email", teacher);
            cmd.Parameters.AddWithValue("@password", teacher.Password);
            cmd.Parameters.AddWithValue("@admin", teacher.Admin);
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

        #region Delete

        /// <summary>
        /// Deletes a teacher from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns> bool </returns>
        public async Task<bool> DeleteTeacherAsync(int id)
        {
            SqlCommand cmd = _sql.Execute("spDeleteTeacher");
            cmd.Parameters.AddWithValue("@id", id);
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
    }
}
