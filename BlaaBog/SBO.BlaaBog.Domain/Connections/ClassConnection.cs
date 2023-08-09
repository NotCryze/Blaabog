using SBO.BlaaBog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Connections
{
    public class ClassConnection
    {
        private SQL _sql;

        public ClassConnection()
        {
            _sql = new SQL();
        }
    }
}
