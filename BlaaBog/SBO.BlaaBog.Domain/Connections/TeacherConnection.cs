using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
