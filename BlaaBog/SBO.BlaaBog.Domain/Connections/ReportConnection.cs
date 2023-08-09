using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Connections
{
    public class ReportConnection
    {
        private SQL _sql;

        public ReportConnection()
        {
            _sql = new SQL();
        }
    }
}
