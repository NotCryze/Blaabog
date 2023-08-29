using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO.BlaaBog.Domain.Entities
{
    public class Comment
    {
        private int? _id;
        public int? Id { get { return _id; } set { _id = value; } }

        private int _authorId;
        public int AuthorId { get { return _authorId; } set { _authorId = value; } }

        private Student _author;
        public Student Author { get { return _author; } set { _author = value; } }

        private int _subjectId;
        public int SubjectId { get { return _subjectId; } set { _subjectId = value; } }

        private Student _subject;
        public Student Subject { get { return _subject; } set { _subject = value; } }

        private string _content;
        public string Content { get { return _content; } set { _content = value; } }

        private bool _approved;
        public bool Approved { get { return _approved; } set { _approved = value; } }

        private int? _approvedById;
        public int? ApprovedById { get { return _approvedById; } set { _approvedById = value; } }

        private Teacher? _approvedBy;
        public Teacher? ApprovedBy { get { return _approvedBy; } set { _approvedBy = value; } }

        private DateTime? _approvedAt;
        public DateTime? ApprovedAt { get { return _approvedAt; } set { _approvedAt = value; } }

        private DateTime _createdAt;
        public DateTime CreatedAt { get { return _createdAt; } set { _createdAt = value; } }

    }
}
