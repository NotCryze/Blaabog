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
        public int? Id { get { return _id; } }

        private int _commentId;
        public int CommentId { get { return _commentId; } }

        private Comment _comment;
        public Comment Comment { get { return _comment; } }

        private string _reason;
        public string Reason { get { return _reason; } }

        private DateTime _createdAt;
        public DateTime CreatedAt { get { return _createdAt; } }

        public Report(int? id, int commentId, string reason, DateTime createdAt)
        {
            _id = id;
            _commentId = commentId;
            _reason = reason;
            _createdAt = createdAt;
        }
    }
}
