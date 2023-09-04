using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Entities
{
    public class Report
    {
        private int? _id;
        public int? Id { get { return _id; } set { _id = value; } }

        private int _commentId;
        public int CommentId { get { return _commentId; } set { _commentId = value; } }

        private Comment _comment;
        public Comment Comment { get { return _comment; } set { _comment = value; } }

        private string _reason;
        public string Reason { get { return _reason; } set { _reason = value; } }

        private DateTime _createdAt;
        public DateTime CreatedAt { get { return _createdAt; } set { _createdAt = value; } }
    }
}
