using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Connections
{
    public class PendingChangeConnection
    {
        private SQL _sql;

        public PendingChangeConnection()
        {
            _sql = new SQL();
        }
    }
}
