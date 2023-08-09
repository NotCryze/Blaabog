using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Connections
{
    public class CommentConnection
    {
        private SQL _sql;

        public CommentConnection()
        {
            _sql = new SQL();
        }
    }
}
