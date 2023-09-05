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
        public int? Id { get { return _id; } set { _id = value; } }

        private string _token;
        public string Token { get { return _token; } set { _token = value; } }

        private int? _teacherId;
        public int? TeacherId { get { return _teacherId; } set { _teacherId = value; } }

        private Teacher _teacher;
        public Teacher Teacher { get { return _teacher; } set { _teacher = value; } }

        private DateTime _createdAt;
        public DateTime CreatedAt { get { return _createdAt; } set { _createdAt = value; } }
    }
}
