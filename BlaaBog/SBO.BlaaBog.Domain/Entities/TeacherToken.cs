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

        private DateTime _created_at;
        public DateTime Created_at { get { return _created_at; } }

    }
}
