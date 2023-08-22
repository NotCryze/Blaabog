using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SBO.BlaaBog.Domain.Entities
{
    public class Student
    {
        private int? _id;
        public int? Id { get { return _id; } }

        private string _name;
        public string Name { get { return _name; } }

        private string _image;
        public string Image { get { return _image; } }

        private string? _description;
        public string? Description { get { return _description; } }

        private string _email;
        public string Email { get { return _email; } }

        private Specialities? _speciality;
        public Specialities? Speciality { get { return _speciality; } }

        private int _classId;
        public int ClassId { get { return _classId; } }

        private Class _class;
        public Class Class { get { return _class; } }

        private DateOnly? _endDate;
        public DateOnly? EndDate { get { return _endDate; } }

        private string _password;
        public string Password { get { return _password; } }

        private List<Comment>? _comments;
        public List<Comment>? Comments { get { return _comments; } }

        public Student(int? id, string name, string image, string? description, string email, Specialities? speciality, int classId, DateOnly? endDate, string password)
        {
            _id = id;
            _name = name;
            _image = image;
            _description = description;
            _email = email;
            _speciality = speciality;
            _classId = classId;
            _endDate = endDate;
            _password = password;
        }
    }
}
