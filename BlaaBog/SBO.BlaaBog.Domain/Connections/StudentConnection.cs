using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
