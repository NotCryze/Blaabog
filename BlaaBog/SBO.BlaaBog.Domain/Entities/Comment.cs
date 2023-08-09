using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Entities
{
    public class Comment
    {
        private int _id;
        public int Id { get { return _id; } }

        private int _authorId;
        public int AuthorId { get { return _authorId; } }

        private Student _author;
        public Student Author { get { return _author; } }

        private int _subjectId;
        public int SubjectId { get { return _subjectId; } }

        private Student _subject;
        public Student Subject { get { return _subject; } }

        private string _content;
        public string Content { get { return _content; } }

        private bool _approved;
        public bool Approved { get { return _approved; } }

        private int? _approvedById;
        public int? ApprovedById { get { return _approvedById; } }

        private Teacher? _approvedBy;
        public Teacher? ApprovedBy { get { return _approvedBy; } }

        private DateTime? _approvedAt;
        public DateTime? ApprovedAt { get { return _approvedAt; } }

        private DateTime _createdAt;
        public DateTime CreatedAt { get { return _createdAt; } }

        public Comment(int id, int authorId, int subjectId, string content, bool approved, int? approvedById, DateTime? approvedAt, DateTime createdAt)
        {
            _id = id;
            _authorId = authorId;
            _subjectId = subjectId;
            _content = content;
            _approved = approved;
            _approvedById = approvedById;
            _approvedAt = approvedAt;
            _createdAt = createdAt;
        }
    }
}
