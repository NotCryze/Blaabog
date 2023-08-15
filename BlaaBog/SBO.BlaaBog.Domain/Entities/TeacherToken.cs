using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Entities
{
    public class TeacherToken
    {
        private int? _id;
        public int? Id { get { return _id; } }

        private string _token;
        public string Token { get { return _token; } }

        private int? _teacherId;
        public int? TeacherId { get { return _teacherId; } }

        private Teacher _teacher;
        public Teacher Teacher { get { return _teacher; } }

        private DateTime _created_at;
        public DateTime Created_at { get { return _created_at; } }

        public TeacherToken(int? id, string token, int? teacherId, DateTime createdAt)
        {
            _id = id;
            _token = token;
            _teacherId = teacherId;
            _created_at = createdAt;
        }

    }
}
