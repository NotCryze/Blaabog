using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Entities
{
    public class Teacher
    {
        private int? _id;
        public int? Id { get { return _id; } set { _id = value; } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        private string _email;
        public string Email { get { return _email; } set { _email = value; } }

        private string _password;
        public string Password { get { return _password; } set { _password = value; } }

        private bool _admin;
        public bool Admin { get { return _admin; } set { _admin = value; } }

        public Teacher(int? id, string name, string email, string password, bool admin)
        {
            _id = id;
            _name = name;
            _email = email;
            _password = password;
            _admin = admin;
        }
    }
}
