using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Entities
{
    public class Class
    {
        private int? _id;
        public int? Id { get { return _id; } }

        public string Name { get { return _startDate.ToString("MM:yyyy"); } }

        private DateOnly _startDate;
        public DateOnly StartDate { get { return _startDate; } }

        private string _token;
        public string Token { get { return _token; } }

        private List<Student>? _students;
        public List<Student>? Students { get { return _students; } }

        public dynamic Dynamic { get; set; }

        public Class(int? id, DateOnly startDate, string token) 
        {
            _id = id;
            _startDate = startDate;
            _token = token;
        }
    }
}
